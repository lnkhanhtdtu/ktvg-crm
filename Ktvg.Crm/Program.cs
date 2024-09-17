using Ktvg.Crm.Factories;
using Ktvg.Crm.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KtvgCrmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KtvgCrmContext")
                         ?? throw new InvalidOperationException("Connection string 'KtvgCrmContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-8.0
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Home/Login";
    options.LogoutPath = "/Home/Logout";
    options.AccessDeniedPath = "/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
});

RepositoryFactory.ConfigureServices(builder.Services);

var app = builder.Build();
app.Initialize();
app.UseItToSeedSqlServer();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();