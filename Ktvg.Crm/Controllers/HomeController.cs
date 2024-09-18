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
        private readonly KtvgCrmContext _context;
        private readonly IAccountService _accountService;

        public HomeController(KtvgCrmContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Account()
        {
            int accountId = Convert.ToInt32(User.FindFirstValue("accountId"));
            var result = _accountService.GetAccount(accountId);

            return View(result);
        }

        [Authorize]
        public IActionResult SaveAccount(EmployeeVM employeeVM)
        {
            var result = _accountService.SaveAccount(employeeVM);
            return View("Account", result);
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
