using Ktvg.Crm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Ktvg.Crm.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Ktvg.Crm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KtvgCrmContext _context;

        public HomeController(ILogger<HomeController> logger, KtvgCrmContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Account()
        {
            var accountId = User.FindFirstValue("accountId");

            var account = _context.Employee
                .Select(x => new EmployeeVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Sex = x.Sex,
                    PhoneNumber = x.PhoneNumber,
                    Username = x.Username,
                    Password = x.Password,
                    Role = x.Role
                }).FirstOrDefault(x => x.Id.ToString() == accountId);

            return View(account);
        }

        [Authorize]
        public IActionResult SaveAccount(EmployeeVM employeeVM)
        {
            var account = _context.Employee.FirstOrDefault(x => x.IsDeleted != true && x.Id == employeeVM.Id);
            if (account == null)
            {
                return View("Account", employeeVM);
            }

            if (employeeVM.FirstName != null)
                account.FirstName = employeeVM.FirstName;
            if (employeeVM.LastName != null)
                account.LastName = employeeVM.LastName;
            if (employeeVM.Sex != null)
                account.Sex = employeeVM.Sex;
            if (employeeVM.PhoneNumber != null)
                account.PhoneNumber = employeeVM.PhoneNumber;
            if (employeeVM.Username != null)
                account.Username = employeeVM.Username;
            if (employeeVM.Password != null)
                account.Password = employeeVM.Password;
            if (employeeVM.Role != null)
                account.Role = employeeVM.Role;

            _context.SaveChanges();

            return View("Account", employeeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Login

        public ActionResult Login(string? returnUrl)
        {
            if (returnUrl != null) ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl)
        {
            ViewBag.returnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var account = _context.Employee.FirstOrDefault(x => x.IsDeleted != false && model.UserName == x.Username && model.Password == x.Password);
                if (account == null)
                {
                    TempData["LoginError"] = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, model.UserName),
                        new Claim(ClaimTypes.Name, account.FirstName),
                        new Claim("accountId", account.Id.ToString()),
                        new Claim("Roles", account.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Cấu hình AuthenticationProperties
                    // AuthenticationProperties authProps = new AuthenticationProperties
                    // {
                    //     IsPersistent = model.RememberMe,
                    //     ExpiresUtc = model.RememberMe ? DateTime.Now.AddDays(1) : (DateTime?)null
                    // };

                    await HttpContext.SignInAsync(claimsPrincipal); // , authProps);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
