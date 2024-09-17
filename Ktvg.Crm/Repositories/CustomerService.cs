using Ktvg.Crm.Models;
using Ktvg.Crm.Repositories.Interfaces;
using Ktvg.Crm.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ktvg.Crm.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly KtvgCrmContext _context;

        public CustomerService(KtvgCrmContext context)
        {
            _context = context;
        }


        public Task<List<CustomerVM>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> CreateCustomerAsync(CustomerVM model)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateCustomerAsync(CustomerVM model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNextCustomerCodeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
