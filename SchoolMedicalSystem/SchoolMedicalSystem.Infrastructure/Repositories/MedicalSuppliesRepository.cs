using Microsoft.EntityFrameworkCore;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Domain.Entities;
using SchoolMedicalSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Infrastructure.Repositories
{
    public class MedicalSuppliesRepository : IMedicalSuppliesRepository
    {
        private readonly SchoolMedicalDbContext _context;

        public MedicalSuppliesRepository(SchoolMedicalDbContext Context) => _context = Context;


        public async Task<Medicine> AddAsync(Medicine entity)
        {
            await _context.Medicines.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Medicines.FindAsync(id);
            if(entity == null)
            {
                return false;
            }
            _context.Medicines.Remove(entity);
            return true;
        }

        public async Task<List<Medicine>> GetAllasync()
        {
            return await _context.Set<Medicine>().Where(x => x.category == "supply").ToListAsync();
        }

        public Task<Medicine?> GetByIdAsync(int id)
        {
            return _context.Set<Medicine>().Where(s => s.category == "supply").FirstOrDefaultAsync(x => x.medicine_id == id);
        }

        public Task<List<Medicine>> GetMedicinesByCategoryAsync(string category)
        {
            return _context.Set<Medicine>()
                .Where(m => m.category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<Medicine> UpdateAsync(Medicine entity)
        {
            _context.Medicines.Update(entity);
            return entity;
        }
    }
}
