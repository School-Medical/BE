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

        public Task<PrescriptionMedicine> AddAsync(PrescriptionMedicine entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PrescriptionMedicine?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(PrescriptionMedicine entity)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteByPrescriptionIdAsync(int prescriptionId)
        {
            var prescription = await _dbContext.PrescriptionMedicines
                .FirstOrDefaultAsync(p => p.prescription_id == prescriptionId);

            if (prescription == null)
            {
                return false; // Prescription not found
            }
            _dbContext.Remove(prescription);
            return true;
        }
    }
}
