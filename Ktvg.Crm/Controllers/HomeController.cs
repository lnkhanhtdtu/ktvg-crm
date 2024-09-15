using Ktvg.Crm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ktvg.Crm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                if (username.Equals("admin") && password.Equals("admin"))
                {
                    // Session["UserName"] = objUser.UserName;
                    return RedirectToAction("Index");
                    // return RedirectToAction("UserDashBoard");
                }
            }

            // Đăng nhập không thành công
            TempData["LoginError"] = "Tên đăng nhập hoặc mật khẩu không chính xác.";

            return View();
        }

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
