namespace Ktvg.Crm.Integrations
{
    public interface IWebApiHttpClientManager
    {
        HttpClient DefaultClient();
        HttpClient AuthorizedClient(string secretKey);
    }
}
