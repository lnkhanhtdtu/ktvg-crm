namespace Ktvg.Crm.Integrations.ZaloAPI
{
    public class ZaloModel
    {
        public string PermissionUrl { get; set; }
        public bool IsOAuth { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
