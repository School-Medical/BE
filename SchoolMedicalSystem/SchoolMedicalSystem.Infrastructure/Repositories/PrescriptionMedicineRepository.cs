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
    public class PrescriptionMedicineRepository : IPrescriptionMedicineRepository
    {
        private readonly SchoolMedicalDbContext _dbContext;
        public PrescriptionMedicineRepository(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PrescriptionMedicine> AddAsync(PrescriptionMedicine entity)
        {
            await _dbContext.PrescriptionMedicines.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.PrescriptionMedicines.FindAsync(id);
            if (entity == null)
                return false;
            _dbContext.PrescriptionMedicines.Remove(entity);
            return true;
        }

        public async Task<PrescriptionMedicine?> GetByIdAsync(int id)
        {
            return await _dbContext.PrescriptionMedicines
                .Include(pm => pm.medicine)
                .Include(pm => pm.prescription)
                .FirstOrDefaultAsync(pm => pm.prescription_medicine_id == id);
        }

        public async Task<bool> UpdateAsync(PrescriptionMedicine entity)
        {
            var existing = await _dbContext.PrescriptionMedicines.FindAsync(entity.prescription_medicine_id);
            if (existing == null)
                return false;

            // Update fields
            existing.prescription_id = entity.prescription_id;
            existing.medicine_id = entity.medicine_id;
            existing.quantity = entity.quantity;

            _dbContext.PrescriptionMedicines.Update(existing);
            return true;
        }

        public async Task<bool> DeleteByPrescriptionIdAsync(int prescriptionId)
        {
            var prescriptionMedicines = await _dbContext.PrescriptionMedicines
                .Where(p => p.prescription_id == prescriptionId)
                .ToListAsync();

            if (!prescriptionMedicines.Any())
                return false;

            _dbContext.PrescriptionMedicines.RemoveRange(prescriptionMedicines);
            return true;
        }

        public async Task<List<PrescriptionMedicine>> GetByPrescriptionId(int prescriptionId)
        {
            return await _dbContext.PrescriptionMedicines
                .Where(pm => pm.prescription_id == prescriptionId)
                .Include(pm => pm.medicine)
                .Include(pm => pm.prescription)
                .ToListAsync();
        }

        public Task<List<PrescriptionMedicine>> AddListAsynce(List<PrescriptionMedicine> list)
        {
            if (list == null || !list.Any())
                throw new ArgumentNullException(nameof(list), "List cannot be null or empty");
            _dbContext.PrescriptionMedicines.AddRange(list);
            return Task.FromResult(list);
        }
    }
}
