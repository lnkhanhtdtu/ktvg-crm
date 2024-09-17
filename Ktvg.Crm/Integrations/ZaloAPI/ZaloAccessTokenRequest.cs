namespace Ktvg.Crm.Integrations.ZaloAPI
{
    public class ZaloAccessTokenRequest
    {
        /// <summary>
        /// Authorization code mà bạn nhận được ở bước 3
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// ID của ứng dụng
        /// </summary>
        public long AppId { get; set; }

        public string GrantType { get; set; }
        public string CodeVerifier { get; set; }
    }
}
