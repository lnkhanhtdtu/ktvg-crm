using Ktvg.Crm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ktvg.Crm.Controllers
{
    [Authorize]
    public class ContactProjectsController : Controller
    {
        private readonly KtvgCrmContext _context;

        public ContactProjectsController(KtvgCrmContext context)
        {
            _context = context;
        }

        // GET: ContactProjects
        public async Task<IActionResult> Index()
        {
            var ktvgCrmContext = _context.ContactProject.Include(c => c.CreatedByEmployee).Include(c => c.DeletedByEmployee).Include(c => c.ModifiedByEmployee);
            return View(await ktvgCrmContext.ToListAsync());
        }

        // GET: ContactProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactProject = await _context.ContactProject
                .Include(c => c.CreatedByEmployee)
                .Include(c => c.DeletedByEmployee)
                .Include(c => c.ModifiedByEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactProject == null)
            {
                return NotFound();
            }

            return View(contactProject);
        }

        // GET: ContactProjects/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id");
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id");
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id");
            return View();
        }

        // POST: ContactProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Remark,CreatedDate,ModifiedDate,DeletedDate,IsDeleted,CreatedById,ModifiedById,DeletedById")] ContactProject contactProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactProject.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactProject.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactProject.ModifiedById);
            return View(contactProject);
        }

        // GET: ContactProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactProject = await _context.ContactProject.FindAsync(id);
            if (contactProject == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactProject.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactProject.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactProject.ModifiedById);
            return View(contactProject);
        }

        // POST: ContactProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Remark,CreatedDate,ModifiedDate,DeletedDate,IsDeleted,CreatedById,ModifiedById,DeletedById")] ContactProject contactProject)
        {
            if (id != contactProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactProjectExists(contactProject.Id))
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
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactProject.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactProject.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", contactProject.ModifiedById);
            return View(contactProject);
        }

        // GET: ContactProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactProject = await _context.ContactProject
                .Include(c => c.CreatedByEmployee)
                .Include(c => c.DeletedByEmployee)
                .Include(c => c.ModifiedByEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactProject == null)
            {
                return NotFound();
            }

            return View(contactProject);
        }

        // POST: ContactProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactProject = await _context.ContactProject.FindAsync(id);
            if (contactProject != null)
            {
                _context.ContactProject.Remove(contactProject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactProjectExists(int id)
        {
            return _context.ContactProject.Any(e => e.Id == id);
        }
    }
}
