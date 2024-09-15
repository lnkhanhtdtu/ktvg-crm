using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ktvg.Crm.Models;

namespace Ktvg.Crm.Controllers
{
    public class ZaloOAuthsController : Controller
    {
        private readonly KtvgCrmContext _context;

        public ZaloOAuthsController(KtvgCrmContext context)
        {
            _context = context;
        }

        // GET: ZaloOAuths
        public async Task<IActionResult> Index()
        {
            return View(await _context.ZaloOAuth.ToListAsync());
        }

        // GET: ZaloOAuths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaloOAuth = await _context.ZaloOAuth
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaloOAuth == null)
            {
                return NotFound();
            }

            return View(zaloOAuth);
        }

        // GET: ZaloOAuths/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ZaloOAuths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,AccessToken,RefreshToken,ExpireIn")] ZaloOAuth zaloOAuth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaloOAuth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zaloOAuth);
        }

        // GET: ZaloOAuths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaloOAuth = await _context.ZaloOAuth.FindAsync(id);
            if (zaloOAuth == null)
            {
                return NotFound();
            }
            return View(zaloOAuth);
        }

        // POST: ZaloOAuths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDate,AccessToken,RefreshToken,ExpireIn")] ZaloOAuth zaloOAuth)
        {
            if (id != zaloOAuth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaloOAuth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaloOAuthExists(zaloOAuth.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(zaloOAuth);
        }

        // GET: ZaloOAuths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaloOAuth = await _context.ZaloOAuth
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaloOAuth == null)
            {
                return NotFound();
            }

            return View(zaloOAuth);
        }

        // POST: ZaloOAuths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaloOAuth = await _context.ZaloOAuth.FindAsync(id);
            if (zaloOAuth != null)
            {
                _context.ZaloOAuth.Remove(zaloOAuth);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaloOAuthExists(int id)
        {
            return _context.ZaloOAuth.Any(e => e.Id == id);
        }
    }
}
