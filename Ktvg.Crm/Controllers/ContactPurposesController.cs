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
    public class ContactPurposesController : Controller
    {
        private readonly KtvgCrmContext _context;

        public ContactPurposesController(KtvgCrmContext context)
        {
            _context = context;
        }

        // GET: ContactPurposes
        public async Task<IActionResult> Index()
        {
            var ktvgCrmContext = _context.ContactPurpose.Include(c => c.CreatedByEmployee).Include(c => c.DeletedByEmployee).Include(c => c.ModifiedByEmployee);
            return View(await ktvgCrmContext.ToListAsync());
        }

        // GET: ContactPurposes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPurpose = await _context.ContactPurpose
                .Include(c => c.CreatedByEmployee)
                .Include(c => c.DeletedByEmployee)
                .Include(c => c.ModifiedByEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactPurpose == null)
            {
                return NotFound();
            }

            return View(contactPurpose);
        }

        // GET: ContactPurposes/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id");
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id");
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id");
            return View();
        }

        // POST: ContactPurposes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Remark,CreatedDate,ModifiedDate,DeletedDate,IsDeleted,CreatedById,ModifiedById,DeletedById")] ContactPurpose contactPurpose)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactPurpose);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactPurpose.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactPurpose.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactPurpose.ModifiedById);
            return View(contactPurpose);
        }

        // GET: ContactPurposes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPurpose = await _context.ContactPurpose.FindAsync(id);
            if (contactPurpose == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactPurpose.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactPurpose.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactPurpose.ModifiedById);
            return View(contactPurpose);
        }

        // POST: ContactPurposes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Remark,CreatedDate,ModifiedDate,DeletedDate,IsDeleted,CreatedById,ModifiedById,DeletedById")] ContactPurpose contactPurpose)
        {
            if (id != contactPurpose.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactPurpose);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactPurposeExists(contactPurpose.Id))
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
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactPurpose.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactPurpose.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactPurpose.ModifiedById);
            return View(contactPurpose);
        }

        // GET: ContactPurposes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPurpose = await _context.ContactPurpose
                .Include(c => c.CreatedByEmployee)
                .Include(c => c.DeletedByEmployee)
                .Include(c => c.ModifiedByEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactPurpose == null)
            {
                return NotFound();
            }

            return View(contactPurpose);
        }

        // POST: ContactPurposes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactPurpose = await _context.ContactPurpose.FindAsync(id);
            if (contactPurpose != null)
            {
                _context.ContactPurpose.Remove(contactPurpose);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactPurposeExists(int id)
        {
            return _context.ContactPurpose.Any(e => e.Id == id);
        }
    }
}
