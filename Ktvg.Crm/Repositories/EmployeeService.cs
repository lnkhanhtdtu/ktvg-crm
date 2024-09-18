using Ktvg.Crm.Models;
using Ktvg.Crm.ViewModels;

namespace Ktvg.Crm
{
    public class EmployeeService : IEmployeeService
    {
        private readonly KtvgCrmContext _context;

        public EmployeeService(KtvgCrmContext context)
        {
            _context = context;
        }

        public List<EmployeeVM> GetEmployees()
        {
            return _context.Employee
                .Where(e => e.IsDeleted != true)
                .Select(e => new EmployeeVM
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Sex = e.Sex,
                    PhoneNumber = e.PhoneNumber,
                    Username = e.Username,
                    Password = e.Password,
                    Role = e.Role
                })
                .ToList();
        }

        public EmployeeVM GetEmployeeVM(int id)
        {
            return _context.Employee
                .Where(e => e.Id == id && e.IsDeleted != true)
                .Select(e => new EmployeeVM
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Sex = e.Sex,
                    PhoneNumber = e.PhoneNumber,
                    Username = e.Username,
                    Password = e.Password,
                    Role = e.Role
                })
                .FirstOrDefault();
        }

        public Employee? GetEmployee(int id)
        {
            return _context.Employee.FirstOrDefault(e => e.Id == id && e.IsDeleted != true);
        }

        public EmployeeVM CreateEmployee(EmployeeVM employeeVM)
        {
            var employee = new Employee
            {
                FirstName = employeeVM.FirstName,
                LastName = employeeVM.LastName,
                Sex = employeeVM.Sex,
                PhoneNumber = employeeVM.PhoneNumber,
                Username = employeeVM.Username,
                Password = employeeVM.Password,
                Role = employeeVM.Role,
                IsDeleted = false
            };

            _context.Employee.Add(employee);
            _context.SaveChanges();

            employeeVM.Id = employee.Id;
            return employeeVM;
        }

        public EmployeeVM UpdateEmployee(EmployeeVM employeeVM)
        {
            var employee = _context.Employee.FirstOrDefault(e => e.Id == employeeVM.Id && e.IsDeleted != true);
            if (employee == null)
            {
                return null;
            }

            employee.FirstName = employeeVM.FirstName;
            employee.LastName = employeeVM.LastName;
            employee.Sex = employeeVM.Sex;
            employee.PhoneNumber = employeeVM.PhoneNumber;
            employee.Username = employeeVM.Username;
            employee.Password = employeeVM.Password;
            employee.Role = employeeVM.Role;

            _context.SaveChanges();

            return employeeVM;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _context.Employee.FirstOrDefault(e => e.Id == id && e.IsDeleted != true);
            if (employee == null)
            {
                return false;
            }

            employee.IsDeleted = true;
            _context.SaveChanges();

            return true;
        }
    }
}
