using Ktvg.Crm.Models;
using Ktvg.Crm.ViewModels;

namespace Ktvg.Crm
{
    public interface IEmployeeService
    {
        List<EmployeeVM> GetEmployees();
        EmployeeVM GetEmployeeVM(int id);
        Employee? GetEmployee(int id);
        EmployeeVM CreateEmployee(EmployeeVM employeeVM);
        EmployeeVM UpdateEmployee(EmployeeVM employeeVM);
        bool DeleteEmployee(int id);
    }
}
