using Newtonsoft.Json;

namespace Ktvg.Crm.Integrations.ZaloAPI
{
    public class ZaloOAuthResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpireIn { get; set; }


        [JsonProperty("error")]
        public int Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        [JsonProperty("msg_id")]
        public string MsgId { get; set; }

        [JsonProperty("sent_time")]
        public string SentTime { get; set; }

        [JsonProperty("sending_mode")]
        public string SendingMode { get; set; }

        [JsonProperty("quota")]
        public Quota Quota { get; set; }
    }

    public class Quota
    {
        [JsonProperty("dailyQuota")]
        public string DailyQuota { get; set; }

        [JsonProperty("remainingQuota")]
        public string RemainingQuota { get; set; }
    }
}
