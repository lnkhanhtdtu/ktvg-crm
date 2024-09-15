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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Account()
        {
            return View();
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
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl)
        {
            // if (ModelState.IsValid)
            // {
            //     if (username.Equals("admin") && password.Equals("admin"))
            //     {
            //         // Session["UserName"] = objUser.UserName;
            //         return RedirectToAction("Index");
            //         // return RedirectToAction("UserDashBoard");
            //     }
            // }
            //
            // // Đăng nhập không thành công
            // TempData["LoginError"] = "Tên đăng nhập hoặc mật khẩu không chính xác.";
            //
            // return View();
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                // var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
                // if (khachHang == null)
                // {
                //     ModelState.AddModelError("loi", "Không có khách hàng này");
                // }
                // else
                // {
                // if (!khachHang.HieuLuc)
                // {
                //     ModelState.AddModelError("loi", "Tài khoản đã bị khóa. Vui lòng liên hệ Admin.");
                // }
                // else
                // {
                if (!model.UserName.Equals("admin") || !model.Password.Equals("admin"))
                {
                    ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
                }
                else
                {
                    var claims = new List<Claim> {
                                new Claim(ClaimTypes.Email, model.UserName),
                                new Claim(ClaimTypes.Name, "Nhựt Khánh"),
                                new Claim("Roles", "Quản trị viên"),

                                //claim - role động
                                // new Claim(ClaimTypes.Role, "Admin")
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

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
                // }
                // }
            }
            return View();
        }

        #endregion

        public ActionResult UserDashBoard()
        {
            // if (Session["UserID"] != null)
            // {
            //     return View("Index");
            // }

            return RedirectToAction("Login");
        }
    }
}
