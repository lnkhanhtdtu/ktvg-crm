using System.Net.Http.Headers;
using Ktvg.Crm.Integrations;

namespace KTVG.Integrations
{
    public class WebApiHttpClientManager : IWebApiHttpClientManager
    {
        private static volatile WebApiHttpClientManager instance;
        private static object syncRoot = new Object();
        private readonly IConfiguration _configuration;

        private WebApiHttpClientManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static WebApiHttpClientManager Instance(IConfiguration configuration)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new WebApiHttpClientManager(configuration);
                    }
                }
            }

            return instance;
        }

        public HttpClient DefaultClient()
        {
            var baseUrl = _configuration.GetValue<string>("ZaloAPI:BaseUrl");
            return new HttpClient() { BaseAddress = new Uri(baseUrl) };
        }

        public HttpClient AuthorizedClient(string secretKey)
        {
            var client = DefaultClient();
            client.DefaultRequestHeaders.Add("secret_key", secretKey);

            return client;
        }
    }
}
