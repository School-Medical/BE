using Microsoft.EntityFrameworkCore;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
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
