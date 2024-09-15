using Ktvg.Crm.Models;
using Microsoft.EntityFrameworkCore;

namespace Ktvg.Crm.Utilities
{
    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<KtvgCrmContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {

            }

            return app;
        }

        public static void Initialize(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<KtvgCrmContext>();
            context.Database.Migrate();
        }

        internal class DbInitializer
        {
            internal static void Initialize(KtvgCrmContext dbContext)
            {
                ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
                dbContext.Database.EnsureCreated();

                var now = DateTime.Now;
                if (!dbContext.ContactProject.Any())
                {
                    var units = new List<ContactProject>
                    {
                        new ContactProject() { CreatedDate = now, Name = "Gọi điện", Icon = "ri-phone-line text-success"},
                        new ContactProject() { CreatedDate = now, Name = "Gặp mặt", Icon = "ri-group-2-line text-warning"}
                    };

                    dbContext.AddRange(units);
                }

                if (!dbContext.ContactPurpose.Any())
                {
                    var units = new List<ContactPurpose>
                    {
                        new ContactPurpose() { CreatedDate = now, Name = "Tương tác tăng MQH" },
                        new ContactPurpose() { CreatedDate = now, Name = "Thu phí" },
                        new ContactPurpose() { CreatedDate = now, Name = "Khách chuyển đổi sang DV khác" },
                        new ContactPurpose() { CreatedDate = now, Name = "Chuyển đồi từ NCC khác" },
                        new ContactPurpose() { CreatedDate = now, Name = "Khác" },
                    };

                    dbContext.AddRange(units);
                }

                dbContext.SaveChanges();
            }
        }
    }
}
