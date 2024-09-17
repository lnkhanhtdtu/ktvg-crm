using Ktvg.Crm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ktvg.Crm.Controllers
{
    [Authorize]
    public class ContactHistoriesController : Controller
    {
        private readonly KtvgCrmContext _context;

        public ContactHistoriesController(KtvgCrmContext context)
        {
            _context = context;
        }

        // GET: ContactHistories
        public async Task<IActionResult> Index()
        {
            var ktvgCrmContext = _context.ContactHistory
                .Include(c => c.ContactProject)
                .Include(c => c.ContactPurpose)
                .Include(c => c.CreatedByEmployee)
                .Include(c => c.DeletedByEmployee)
                .Include(c => c.ModifiedByEmployee);
            return View(await ktvgCrmContext.ToListAsync());
        }

        // GET: ContactHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactHistory = await _context.ContactHistory
                .Include(c => c.ContactProject)
                .Include(c => c.ContactPurpose)
                .Include(c => c.CreatedByEmployee)
                .Include(c => c.DeletedByEmployee)
                .Include(c => c.ModifiedByEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactHistory == null)
            {
                return NotFound();
            }

            return View(contactHistory);
        }

        // GET: ContactHistories/Create
        public IActionResult Create()
        {
            ViewData["ContactProjectId"] = new SelectList(_context.ContactProject, "Id", "Id");
            ViewData["ContactPurposeId"] = new SelectList(_context.ContactPurpose, "Id", "Id");
            ViewData["CreatedById"] = new SelectList(_context.Employee, "Id", "Id");
            ViewData["DeletedById"] = new SelectList(_context.Employee, "Id", "Id");
            ViewData["ModifiedById"] = new SelectList(_context.Employee, "Id", "Id");
            return View();
        }

        // POST: ContactHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactProjectId,ContactPurposeId,Reason,StartDate,RescheduleDate,Status,Id,CreatedDate,ModifiedDate,DeletedDate,IsDeleted,Remark,CreatedById,ModifiedById,DeletedById")] ContactHistory contactHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactProjectId"] = new SelectList(_context.ContactProject, "Id", "Id", contactHistory.ContactProjectId);
            ViewData["ContactPurposeId"] = new SelectList(_context.ContactPurpose, "Id", "Id", contactHistory.ContactPurposeId);
            ViewData["CreatedById"] = new SelectList(_context.Employee, "Id", "Id", contactHistory.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Employee, "Id", "Id", contactHistory.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Employee, "Id", "Id", contactHistory.ModifiedById);
            return View(contactHistory);
        }

        // GET: ContactHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactHistory = await _context.ContactHistory.FindAsync(id);
            if (contactHistory == null)
            {
                return NotFound();
            }
            ViewData["ContactProjectId"] = new SelectList(_context.ContactProject, "Id", "Id", contactHistory.ContactProjectId);
            ViewData["ContactPurposeId"] = new SelectList(_context.ContactPurpose, "Id", "Id", contactHistory.ContactPurposeId);
            ViewData["CreatedById"] = new SelectList(_context.Employee, "Id", "Id", contactHistory.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Employee, "Id", "Id", contactHistory.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Employee, "Id", "Id", contactHistory.ModifiedById);
            return View(contactHistory);
        }

        // POST: ContactHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactProjectId,ContactPurposeId,Reason,StartDate,RescheduleDate,Status,Id,CreatedDate,ModifiedDate,DeletedDate,IsDeleted,Remark,CreatedById,ModifiedById,DeletedById")] ContactHistory contactHistory)
        {
            if (id != contactHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactHistoryExists(contactHistory.Id))
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
            ViewData["ContactProjectId"] = new SelectList(_context.ContactProject, "Id", "Id", contactHistory.ContactProjectId);
            ViewData["ContactPurposeId"] = new SelectList(_context.ContactPurpose, "Id", "Id", contactHistory.ContactPurposeId);
            ViewData["CreatedById"] = new SelectList(_context.Employee, "Id", "Id", contactHistory.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Employee, "Id", "Id", contactHistory.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Employee, "Id", "Id", contactHistory.ModifiedById);
            return View(contactHistory);
        }

        // GET: ContactHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactHistory = await _context.ContactHistory
                .Include(c => c.ContactProject)
                .Include(c => c.ContactPurpose)
                .Include(c => c.CreatedByEmployee)
                .Include(c => c.DeletedByEmployee)
                .Include(c => c.ModifiedByEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactHistory == null)
            {
                return NotFound();
            }

            return View(contactHistory);
        }

        // POST: ContactHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactHistory = await _context.ContactHistory.FindAsync(id);
            if (contactHistory != null)
            {
                _context.ContactHistory.Remove(contactHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactHistoryExists(int id)
        {
            return _context.ContactHistory.Any(e => e.Id == id);
        }
    }
}
