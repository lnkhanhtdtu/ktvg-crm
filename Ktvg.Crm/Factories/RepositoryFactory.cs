using Ktvg.Crm.Integrations.ZaloAPI;
using Ktvg.Crm.Repositories;
using Ktvg.Crm.Repositories.Interfaces;
using KTVG.Integrations.ZaloAPI;

namespace Ktvg.Crm.Factories
{
    public class RepositoryFactory
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IContactProjectService, ContactProjectService>();
            services.AddTransient<IContactPurposeService, ContactPurposeService>();
            services.AddTransient<IMessageLogService, MessageLogService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IZaloService, ZaloService>();
        }
    }
}
