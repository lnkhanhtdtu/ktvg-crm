using Ktvg.Crm.Models;

namespace Ktvg.Crm
{
    public interface IContactProjectService
    {
        Task<List<ContactProject>> GetAllAsync();
        Task<ContactProject> GetByIdAsync(int id);
        Task<ContactProject> CreateAsync(ContactProject model);
        Task<ContactProject> UpdateAsync(ContactProject model);
        Task DeleteAsync(int id);
    }
}
