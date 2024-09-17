using Ktvg.Crm.Models;

namespace Ktvg.Crm.Integrations.ZaloAPI
{
    public interface IZaloService
    {
        Task<ZaloOAuthResponse?> GetAccessTokenFirstTime(string code);
        Task<ZaloOAuthResponse> GetAccessTokenFromRefreshToken();
        Task SendZaloZns(Customer model);
        Task<bool> CheckAccessToken();
        Task<string> GetAccessTokenFromDb();
        Task SendRenewalReminderMessages(List<Customer> locates);
    }
}
