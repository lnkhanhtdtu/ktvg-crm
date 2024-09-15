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
    public class EmployeesController : Controller
    {
        private readonly KtvgCrmContext _context;

        public EmployeesController(KtvgCrmContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var ktvgCrmContext = _context.Employee.Include(e => e.CreatedByEmployee).Include(e => e.DeletedByEmployee).Include(e => e.ModifiedByEmployee);
            return View(await ktvgCrmContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.CreatedByEmployee)
                .Include(e => e.DeletedByEmployee)
                .Include(e => e.ModifiedByEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Employee, "Id", "Id");
            ViewData["DeletedById"] = new SelectList(_context.Employee, "Id", "Id");
            ViewData["ModifiedById"] = new SelectList(_context.Employee, "Id", "Id");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Sex,PhoneNumber,Username,Password,Role,Id,CreatedDate,ModifiedDate,DeletedDate,IsDeleted,Remark,CreatedById,ModifiedById,DeletedById")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Employee, "Id", "Id", employee.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Employee, "Id", "Id", employee.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Employee, "Id", "Id", employee.ModifiedById);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Employee, "Id", "Id", employee.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Employee, "Id", "Id", employee.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Employee, "Id", "Id", employee.ModifiedById);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Sex,PhoneNumber,Username,Password,Role,Id,CreatedDate,ModifiedDate,DeletedDate,IsDeleted,Remark,CreatedById,ModifiedById,DeletedById")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["CreatedById"] = new SelectList(_context.Employee, "Id", "Id", employee.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Employee, "Id", "Id", employee.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Employee, "Id", "Id", employee.ModifiedById);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.CreatedByEmployee)
                .Include(e => e.DeletedByEmployee)
                .Include(e => e.ModifiedByEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
