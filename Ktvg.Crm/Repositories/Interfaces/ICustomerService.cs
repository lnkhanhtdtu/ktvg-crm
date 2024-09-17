using Ktvg.Crm.Models;
using Ktvg.Crm.ViewModels;

namespace Ktvg.Crm.Repositories.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerVM>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(CustomerVM model);
        Task<Customer> UpdateCustomerAsync(CustomerVM model);
        Task DeleteCustomerAsync(int id);
        Task<string> GetNextCustomerCodeAsync();
    }
}
