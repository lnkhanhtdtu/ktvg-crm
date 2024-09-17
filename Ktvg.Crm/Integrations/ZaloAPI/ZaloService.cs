using System.Net;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Ktvg.Crm.Integrations.ZaloAPI;
using Ktvg.Crm.Models;

namespace KTVG.Integrations.ZaloAPI
{
    public class ZaloService : IZaloService
    {
        private readonly KtvgCrmContext _context;
        private readonly IConfiguration _configuration;

        public ZaloService(IConfiguration configuration, KtvgCrmContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<ZaloOAuthResponse?> GetAccessTokenFirstTime(string code)
        {
            try
            {
                var oauthUrl = _configuration.GetValue<string>("ZaloAPI:OauthUrl");
                var secretKey = _configuration.GetValue<string>("ZaloAPI:SecretKey");
                var appId = _configuration.GetValue<string>("ZaloAPI:AppId");
                var codeVerifier = _configuration.GetValue<string>("ZaloAPI:CodeVerifier");

                var client = WebApiHttpClientManager.Instance(_configuration).AuthorizedClient(secretKey);

                var data = new Dictionary<string, string>
                {
                    { "code", code },
                    { "app_id", appId },
                    { "grant_type", "authorization_code" },
                    { "code_verifier", codeVerifier }
                };

                var content = new FormUrlEncodedContent(data);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await client.PostAsync(oauthUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ZaloOAuthResponse>(responseString);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ZaloOAuthResponse> GetAccessTokenFromRefreshToken()
        {
            var secretKey = _configuration.GetValue<string>("ZaloAPI:SecretKey");
            var appId = _configuration.GetValue<string>("ZaloAPI:AppId");

            var client = new HttpClient();
            var request1 = new HttpRequestMessage(HttpMethod.Post, "https://oauth.zaloapp.com/v4/oa/access_token");
            request1.Headers.Add("secret_key", secretKey);

            var refreshToken = await GetRefreshTokenFromDb();
            

            var collection = new List<KeyValuePair<string, string>>
            {
                new("refresh_token", refreshToken),
                new("app_id", appId),
                new("grant_type", "refresh_token")
            };

            var content = new FormUrlEncodedContent(collection);
            request1.Content = content;
            var response = await client.SendAsync(request1);
            var result = new ZaloOAuthResponse();

            if (response.EnsureSuccessStatusCode().StatusCode == HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ZaloOAuthResponse>(responseString);
                var now = DateTime.Now;

                var zaloOAuth = new ZaloOAuth()
                {
                    CreatedDate = now,
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken,
                    ExpireIn = result.ExpireIn
                };

                _context.ZaloOAuth.Add(zaloOAuth);
                await _context.SaveChangesAsync();
            }

            return result;
        }


        public async Task SendZaloZns(Customer model)
        {
            try
            {
                var accessToken = await GetAccessTokenFromDb();
                var templateId = "366904"; //model.LocateType == LocateType.NewRegistration ? "362076" : "364206"; // Xác nhận thanh toán (Đăng ký mới) | Xác nhận thanh toán (Gia hạn 1)
                // Gửi yêu cầu tới Zalo với accessToken từ DB
                var result = await SendZaloRequest(model, accessToken, templateId);

                // Nếu gặp lỗi, thử với accessToken từ refresh token
                if (result.Error != 0)
                {
                    var refreshToken = await GetAccessTokenFromRefreshToken();
                    result = await SendZaloRequest(model, refreshToken.AccessToken, templateId);
                }

                // Cập nhật thông tin lỗi hoặc thành công
                // model.Error = result.Error;
                // model.Message = result.Message;
                model.IsSendZalo = result.Error == 0;
            }
            catch (Exception ex)
            {
                // model.Message = "Lỗi: " + ex.Message;
            }
        }

        public async Task<string> GetAccessTokenFromDb()
        {
            var zaloOAuth = await _context.ZaloOAuth.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (zaloOAuth == null || zaloOAuth.IsExpire())
            {
                // Lấy lại Token
                return "";

            }

            return zaloOAuth.AccessToken;
        }

        // Phương thức để tạo payload và gửi yêu cầu tới Zalo API
        public async Task SendRenewalReminderMessages(List<Customer> locates)
        {
            try
            {
                var accessToken = await GetAccessTokenFromDb();

                // Gửi yêu cầu tới Zalo với accessToken từ DB
                var result = await SendRenewalReminderMessagesRequest(locates, accessToken);

                // Nếu gặp lỗi, thử với accessToken từ refresh token
                if (result.Error != 0)
                {
                    var refreshToken = await GetAccessTokenFromRefreshToken();
                    result = await SendRenewalReminderMessagesRequest(locates, refreshToken.AccessToken);
                }

                // TODO: Cần xử lý lưu log

                // Cập nhật thông tin lỗi hoặc thành công
                // model.Error = result.Error;
                // model.Message = result.Message;
                // model.IsSendZalo = result.Error == 0;
            }
            catch (Exception ex)
            {
                // model.Message = "Lỗi: " + ex.Message;
            }
        }

        private async Task<ZaloOAuthResponse> SendRenewalReminderMessagesRequest(List<Customer> locates, string accessToken)
        {
            foreach (var model in locates)
            {
                await SendZaloRequest(model, accessToken, ""); // Template nhắc nhở gia hạn
            }

            return null;
        }

        private async Task<ZaloOAuthResponse> SendZaloRequest(Customer model, string accessToken, string templateId)
        {
            var payload = new PaymentConfirmationNewRegistration()
            {
                Phone = model.PhoneNumber.Substring(1),
                TemplateId = templateId,
                TemplateData = new TemplateDataNew()
                {
                    NgayBatDauDv = model.RegistrationDate.ToString("dd/MM/yyyy"),
                    MaDonHang = $"DH{DateTime.Now.ToString("yyyyMMddHHmmss")}",
                    CustomerName = model.CustomerName
                }
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, "https://business.openapi.zalo.me/message/template");
            request.Headers.Add("access_token", accessToken);
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ZaloOAuthResponse>(responseString);

            await RecordMessageLog(model.Id, jsonPayload, responseString, result);

            return result;
        }

        private async Task RecordMessageLog(int customerId, string requestPayload, string responsePayload, ZaloOAuthResponse result)
        {
            var log = new MessageLog()
            {
                CustomerId = customerId,
                Type = MessageType.Zalo,
                Recipient = "",
                Content = "",
                SentTime = DateTime.Now,
                RequestPayload = requestPayload,
                ResponsePayload = responsePayload,
                IsSuccessful = result.Error == 0,
                ErrorMessage = result.Message
            };

            _context.MessageLog.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetRefreshTokenFromDb()
        {
            var zaloOAuth = await _context.ZaloOAuth.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (zaloOAuth == null || zaloOAuth.IsExpire())
            {
                // Lấy lại Token
                return "";

            }

            return zaloOAuth.RefreshToken;
        }

        public async Task<bool> CheckAccessToken()
        {
            var zaloOAuth = await _context.ZaloOAuth.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return !(zaloOAuth == null || zaloOAuth.IsExpire());
        }

        // Xác nhận thanh toán (Đăng ký mới)
        public class PaymentConfirmationNewRegistration
        {
            [JsonProperty("phone")]
            public string Phone { get; set; }

            [JsonProperty("template_id")]
            public string TemplateId { get; set; }

            [JsonProperty("template_data")]
            public TemplateDataNew TemplateData { get; set; }

            [JsonProperty("tracking_id")]
            public string TrackingId { get; set; }
        }

        public class TemplateData
        {
            [JsonProperty("ngay_tt")]
            public string RegistrationDate { get; set; }

            [JsonProperty("name")]
            public string CustomerName { get; set; }

            [JsonProperty("so_xe")]
            public string VehicleNumber { get; set; }

            [JsonProperty("thoi_gian")]
            public string RegistrationNewDate { get; set; }

            [JsonProperty("so_tien")]
            public decimal PaymentAmount { get; set; }
        }

        public class TemplateDataNew
        {
            [JsonProperty("ngay_bat_dau_dv")]
            public string NgayBatDauDv { get; set; }

            [JsonProperty("ma_don_hang")]
            public string MaDonHang { get; set; }

            [JsonProperty("customerName")]
            public string CustomerName { get; set; }
        }
    }
}
