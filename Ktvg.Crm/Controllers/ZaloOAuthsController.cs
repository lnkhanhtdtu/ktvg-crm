using Azure.Core;
using Ktvg.Crm.Integrations.ZaloAPI;
using Ktvg.Crm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ktvg.Crm.Controllers
{
    [Authorize]
    public class ZaloOAuthsController : Controller
    {
        private readonly KtvgCrmContext _context;
        private readonly IZaloService _zaloService;

        public ZaloOAuthsController(KtvgCrmContext context, IZaloService zaloService)
        {
            _context = context;
            _zaloService = zaloService;
        }

        // GET: ZaloOAuths
        public async Task<IActionResult> Index()
        {
            var zalo = InitializeZaloModel();

            var zaloOAuth = _context.ZaloOAuth.OrderByDescending(x => x.CreatedDate).FirstOrDefault();

            zalo.IsOAuth = zaloOAuth != null && !zaloOAuth.IsExpire();
            zalo.AccessToken = zaloOAuth.AccessToken;
            zalo.RefreshToken = zaloOAuth.RefreshToken;

            return View(zalo);
        }

        [HttpGet]
        [Route("admin/zalo/callback")]
        public async Task<ActionResult> Callback(string oa_id, string code)
        {
            var zalo = InitializeZaloModel();

            if (!string.IsNullOrEmpty(code))
            {
                var zaloOAuthResponse = await _zaloService.GetAccessTokenFirstTime(code);

                if (zaloOAuthResponse != null && !string.IsNullOrEmpty(zaloOAuthResponse.AccessToken))
                {
                    zalo.IsOAuth = true;
                    await SaveZaloOAuth(zaloOAuthResponse);
                    ViewBag.Message = "Xác thực thành công";
                }
                else
                {
                    ViewBag.Message = "Xác thực thất bại";
                }
            }
            else
            {
                ViewBag.Message = "Xác thực thất bại";
            }

            return View("Index", zalo);
        }

        [HttpPost]
        public async Task<ActionResult> SaveAccessTokenManually(ZaloModel zalo)
        {
            var zaloOAuth = new ZaloOAuth()
            {
                CreatedDate = DateTime.Now,
                AccessToken = zalo.AccessToken,
                RefreshToken = zalo.RefreshToken,
                ExpireIn = "90000"
            };

            _context.ZaloOAuth.Add(zaloOAuth);
            await _context.SaveChangesAsync();
            
            return View("Index", zalo);
        }

        private ZaloModel InitializeZaloModel()
        {
            var permissionUrl = "https://oauth.zaloapp.com/v4/oa/permission?app_id=2117687196653409715&redirect_uri=https%3A%2F%2Fktvinagroup.com%2Fadmin%2Fzalo%2Fcallback";
            return new ZaloModel() { PermissionUrl = permissionUrl, IsOAuth = false };
        }

        private async Task SaveZaloOAuth(ZaloOAuthResponse zaloOAuthResponse)
        {
            var zaloOAuth = new ZaloOAuth()
            {
                CreatedDate = DateTime.Now,
                AccessToken = zaloOAuthResponse.AccessToken,
                RefreshToken = zaloOAuthResponse.RefreshToken,
                ExpireIn = zaloOAuthResponse.ExpireIn
            };

            _context.ZaloOAuth.Add(zaloOAuth);
            await _context.SaveChangesAsync();
        }
    }
}
