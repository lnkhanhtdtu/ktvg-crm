using Ktvg.Crm.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Ktvg.Crm
{
    public class AccountService : IAccountService
    {
        private readonly KtvgCrmContext _context;
        private readonly IEmployeeService _employeeService;

        public AccountService(KtvgCrmContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        public EmployeeVM GetAccount(int accountId)
        {
            var account = _employeeService.GetEmployeeVM(accountId);
            return account;
        }

        public EmployeeVM SaveAccount(EmployeeVM employeeVM)
        {
            var account = _employeeService.GetEmployee(employeeVM.Id);
            if (account == null)
            {
                return employeeVM;
            }

            if (employeeVM.FirstName != null)
                account.FirstName = employeeVM.FirstName;
            if (employeeVM.LastName != null)
                account.LastName = employeeVM.LastName;
            if (employeeVM.Sex != null)
                account.Sex = employeeVM.Sex;
            if (employeeVM.PhoneNumber != null)
                account.PhoneNumber = employeeVM.PhoneNumber;
            if (employeeVM.Username != null)
                account.Username = employeeVM.Username;
            if (employeeVM.Password != null)
                account.Password = employeeVM.Password;
            if (employeeVM.Role != null)
                account.Role = employeeVM.Role;

            _context.SaveChanges();

            return employeeVM;
        }
    }
}
