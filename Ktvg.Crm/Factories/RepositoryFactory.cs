using Ktvg.Crm.Integrations.ZaloAPI;
using KTVG.Integrations.ZaloAPI;

namespace Ktvg.Crm.Factories
{
    public class RepositoryFactory
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IZaloService, ZaloService>();
        }
    }
}
