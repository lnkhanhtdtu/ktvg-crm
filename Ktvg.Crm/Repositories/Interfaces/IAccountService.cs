using Ktvg.Crm.ViewModels;

namespace Ktvg.Crm
{
    public interface IAccountService
    {
        EmployeeVM GetAccount(int accountId);
        EmployeeVM SaveAccount(EmployeeVM employeeVM);
    }
}
