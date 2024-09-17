using Ktvg.Crm.Integrations.ZaloAPI;
using Ktvg.Crm.Models;
using Ktvg.Crm.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ktvg.Crm.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly KtvgCrmContext _context;
        private readonly IZaloService _zaloService;

        public CustomersController(KtvgCrmContext context, IZaloService zaloService)
        {
            _context = context;
            _zaloService = zaloService;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            ViewData["IsValidToken"] = await _zaloService.CheckAccessToken();

            // Load necessary data for dropdowns
            ViewData["ProjectList"] = await _context.Set<ContactProject>().ToListAsync();
            ViewData["ContactProjects"] = new SelectList(await _context.Set<ContactProject>().ToListAsync(), "Id", "Name");
            ViewData["ContactPurposes"] = new SelectList(await _context.Set<ContactPurpose>().ToListAsync(), "Id", "Name");

            // Fetch and convert customer data
            var customers = await _context.Customer
                .Where(x => x.IsDeleted != true)
                .Select(x => Customer.ConvertToCustomerVM(x))
                .ToListAsync();

            return View(customers);
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
        private int GetCreatedById()
        {
            string accountIdString = User.FindFirstValue("accountId");
            if (int.TryParse(accountIdString, out int createdById))
            {
                return createdById;
            }
            throw new InvalidOperationException("Invalid or missing accountId");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var customer = model.Id > 0 ? await _context.Customer.FindAsync(model.Id) : new Customer();
                if (customer == null && model.Id > 0)
                {
                    return NotFound();
                }

                UpdateCustomerFromModel(customer, model);

                if (model.Id == 0)
                {
                    _context.Customer.Add(customer);
                }
                else
                {
                    _context.Update(customer);
                }

                await _context.SaveChangesAsync();

                if (model.SendZaloConfirmation)
                {
                    await _zaloService.SendZaloZns(customer);
                }

                TempData["RegistrationMessage"] = model.Id > 0 ? "Cập nhật khách hàng thành công." : "Tạo khách hàng thành công.";
            }
            catch (Exception)
            {
                TempData["RegistrationError"] = "Xử lý khách hàng thất bại.";
            }

            return RedirectToAction(nameof(Index));
        }

        private void UpdateCustomerFromModel(Customer customer, CustomerVM model)
        {
            customer.RegistrationDate = model.RegistrationDate;
            customer.ProductName = model.InstallationType == "GPS" ? "Thiết bị giám sát hành trình" : "Camera nghị định";
            customer.VehicleType = model.VehicleType;
            customer.VehicleNumber = model.VehicleNumber;
            customer.CustomerSource = model.CustomerSource;
            customer.CustomerCode = model.CustomerCode;
            customer.CustomerName = model.CustomerName;
            customer.PhoneNumber = model.PhoneNumber;
            customer.CustomerAddress = model.CustomerAddress;
            customer.DeviceInstalled = model.DeviceInstalled;
            customer.InstallationType = model.InstallationType;
            customer.PaymentAmount = model.PaymentAmount;
            customer.Remark = model.Remark;
            customer.LocateType = model.LocateType;

            if (customer.Id == 0)
            {
                customer.CreatedById = GetCreatedById();
                customer.CreatedDate = DateTime.Now;
                customer.IsDeleted = false;
            }
            else
            {
                customer.ModifiedDate = DateTime.Now;
                customer.ModifiedById = GetCreatedById();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RecordContactHistory(ContactHistoryVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contactHistory = new ContactHistory()
                    {
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        // CreatedById = UserSession.Instance.UserData.Id,

                        ContactProjectId = model.ContactProjectId,
                        ContactPurposeId = model.ContactPurposeId,
                        CustomerId = model.CustomerId,
                        Reason = model.Reason,
                        StartDate = model.StartDate,
                        RescheduleDate = model.RescheduleDate,
                        Status = model.Status,
                        Result = model.Result,
                        Remark = model.Remark,
                    };

                    // Lưu dữ liệu vào cơ sở dữ liệu
                    _context.ContactHistory.Add(contactHistory);
                    await _context.SaveChangesAsync();

                    TempData["CreateRegistrationSuccess"] = "Tạo lịch sử thành công.";
                }
            }
            catch (Exception e)
            {
                TempData["CreateRegistrationError"] = "Tạo lịch sử thất bại.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RecordContactHistory(int customerId)
        {
            var contactHistory = _context.ContactHistory
                .Include(x => x.Customer)
                .Include(x => x.ContactProject)
                .Include(x => x.ContactPurpose)
                .Where(x => x.IsDeleted != true && x.CustomerId == customerId)
                .ToList();

            return View(contactHistory);
        }

        public IActionResult GetCustomerDetails(int id)
        {
            var customer = _context.Customer.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Json(customer);
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

        [HttpGet]
        public JsonResult GetNextCustomerCode()
        {
            var lastCustomer = _context.Customer
                .OrderByDescending(c => c.CustomerCode)
                .FirstOrDefault();

            string nextCustomerCode;

            if (lastCustomer == null)
            {
                // Nếu không có khách hàng nào, bắt đầu từ mã mặc định
                nextCustomerCode = "KH00001";
            }
            else
            {
                // Lấy phần số của CustomerCode sau ký tự "KH"
                string lastCode = lastCustomer.CustomerCode;
                if (lastCode.Length > 2 && int.TryParse(lastCode.Substring(2), out int lastNumber))
                {
                    // Tăng số lên 1 và tạo mã khách hàng mới
                    nextCustomerCode = $"KH{(lastNumber + 1):D5}";
                }
                else
                {
                    // Nếu mã không hợp lệ, bắt đầu từ mã mặc định
                    nextCustomerCode = "KH00001";
                }
            }

            // Trả về mã khách hàng mới dưới dạng JSON
            return Json(new { nextCode = nextCustomerCode });
        }

    }
}
