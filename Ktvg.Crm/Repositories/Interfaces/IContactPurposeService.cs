using Ktvg.Crm.Models;

namespace Ktvg.Crm
{
    public interface IContactPurposeService
    {
        Task<List<ContactPurpose>> GetAllAsync();
        Task<ContactPurpose> GetByIdAsync(int id);
        Task<ContactPurpose> CreateAsync(ContactPurpose model);
        Task<ContactPurpose> UpdateAsync(ContactPurpose model);
        Task DeleteAsync(int id);
    }
}
