using Ktvg.Crm.Models;
using Microsoft.EntityFrameworkCore;

namespace Ktvg.Crm
{
    public class ContactPurposeService : IContactPurposeService
    {
        private readonly KtvgCrmContext _context;

        public ContactPurposeService(KtvgCrmContext context)
        {
            _context = context;
        }

        public async Task<List<ContactPurpose>> GetAllAsync()
        {
            return await _context.ContactPurpose.Where(x => x.IsDeleted != true).ToListAsync();
        }

        public async Task<ContactPurpose> GetByIdAsync(int id)
        {
            return await _context.ContactPurpose.FindAsync(id);
        }

        public async Task<ContactPurpose> CreateAsync(ContactPurpose model)
        {
            _context.ContactPurpose.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ContactPurpose> UpdateAsync(ContactPurpose model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var contactPurpose = await _context.ContactPurpose.FindAsync(id);
            if (contactPurpose != null)
            {
                _context.ContactPurpose.Remove(contactPurpose);
                await _context.SaveChangesAsync();
            }
        }
    }
}
