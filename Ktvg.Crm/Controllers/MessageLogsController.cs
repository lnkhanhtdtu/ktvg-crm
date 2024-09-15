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
    public class MessageLogsController : Controller
    {
        private readonly KtvgCrmContext _context;

        public MessageLogsController(KtvgCrmContext context)
        {
            _context = context;
        }

        // GET: MessageLogs
        public async Task<IActionResult> Index()
        {
            var ktvgCrmContext = _context.MessageLog.Include(m => m.Customer);
            return View(await ktvgCrmContext.ToListAsync());
        }

        // GET: MessageLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageLog = await _context.MessageLog
                .Include(m => m.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageLog == null)
            {
                return NotFound();
            }

            return View(messageLog);
        }

        // GET: MessageLogs/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id");
            return View();
        }

        // POST: MessageLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,Type,Recipient,Content,SentTime,RequestPayload,ResponsePayload,IsSuccessful,ErrorMessage")] MessageLog messageLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messageLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", messageLog.CustomerId);
            return View(messageLog);
        }

        // GET: MessageLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageLog = await _context.MessageLog.FindAsync(id);
            if (messageLog == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", messageLog.CustomerId);
            return View(messageLog);
        }

        // POST: MessageLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,Type,Recipient,Content,SentTime,RequestPayload,ResponsePayload,IsSuccessful,ErrorMessage")] MessageLog messageLog)
        {
            if (id != messageLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageLogExists(messageLog.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", messageLog.CustomerId);
            return View(messageLog);
        }

        // GET: MessageLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageLog = await _context.MessageLog
                .Include(m => m.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageLog == null)
            {
                return NotFound();
            }

            return View(messageLog);
        }

        // POST: MessageLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messageLog = await _context.MessageLog.FindAsync(id);
            if (messageLog != null)
            {
                _context.MessageLog.Remove(messageLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageLogExists(int id)
        {
            return _context.MessageLog.Any(e => e.Id == id);
        }
    }
}
