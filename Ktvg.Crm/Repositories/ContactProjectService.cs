using Ktvg.Crm.Models;
using Microsoft.EntityFrameworkCore;

namespace Ktvg.Crm
{
    public class ContactProjectService : IContactProjectService
    {
        private readonly KtvgCrmContext _context;

        public ContactProjectService(KtvgCrmContext context)
        {
            _context = context;
        }

        public async Task<List<ContactProject>> GetAllAsync()
        {
            return await _context.ContactProject.Where(x => x.IsDeleted != true).ToListAsync();
        }

        public async Task<ContactProject> GetByIdAsync(int id)
        {
            return await _context.ContactProject.FindAsync(id);
        }

        public async Task<ContactProject> CreateAsync(ContactProject model)
        {
            _context.ContactProject.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ContactProject> UpdateAsync(ContactProject model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var contactProject = await _context.ContactProject.FindAsync(id);
            if (contactProject != null)
            {
                _context.ContactProject.Remove(contactProject);
                await _context.SaveChangesAsync();
            }
        }
    }
}
