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
    public class CustomersController : Controller
    {
        private readonly KtvgCrmContext _context;

        public CustomersController(KtvgCrmContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            ViewData["ProjectList"] = _context.Set<ContactProject>().ToList(); // new SelectList(, "Id", "Name");

            var result = await _context.Customer
                .ToListAsync();
            return View(result);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.CreatedByEmployee)
                .Include(c => c.DeletedByEmployee)
                .Include(c => c.ModifiedByEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id");
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id");
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationDate,ProductName,VehicleType,VehicleNumber,CustomerName,CustomerAddress,PhoneNumber,IsActive,PaymentAmount,HasZalo,DeviceInstalled,InstallationType,LocateType,IsSendZalo,IsSendSms,Id,CreatedDate,ModifiedDate,DeletedDate,IsDeleted,Remark,CreatedById,ModifiedById,DeletedById")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", customer.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", customer.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", customer.ModifiedById);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", customer.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", customer.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", customer.ModifiedById);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationDate,ProductName,VehicleType,VehicleNumber,CustomerName,CustomerAddress,PhoneNumber,IsActive,PaymentAmount,HasZalo,DeviceInstalled,InstallationType,LocateType,IsSendZalo,IsSendSms,Id,CreatedDate,ModifiedDate,DeletedDate,IsDeleted,Remark,CreatedById,ModifiedById,DeletedById")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["CreatedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", customer.CreatedById);
            ViewData["DeletedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", customer.DeletedById);
            ViewData["ModifiedById"] = new SelectList(_context.Set<Employee>(), "Id", "Id", customer.ModifiedById);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.CreatedByEmployee)
                .Include(c => c.DeletedByEmployee)
                .Include(c => c.ModifiedByEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
