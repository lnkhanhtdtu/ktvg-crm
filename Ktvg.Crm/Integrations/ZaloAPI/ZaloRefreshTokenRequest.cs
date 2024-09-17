namespace Ktvg.Crm.Integrations.ZaloAPI
{
    public class ZaloRefreshTokenRequest
    {
        public string RefreshToken { get; set; }

        /// <summary>
        /// ID của ứng dụng
        /// </summary>
        public long AppId { get; set; }

        public string GrantType { get; set; }
    }
}
