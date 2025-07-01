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
    public class MedicationRepository : GenericRepository<Medication>, IMedicationRepository 
    {
        public MedicationRepository(SchoolMedicalDbContext context) : base(context)
        {
        }

        public async Task<Medication?> GetByNameAsync(string name)
        {
            return await _context.Medications
                .FirstOrDefaultAsync(m => m.medicine_name.ToLower() == name.ToLower());
        }

        public async Task<List<Medication>> GetByGivenDoseId(int id)
        {
            return await _context.Medications.Where(m => m.given_dose_id == id).ToListAsync();
        }

    }
}
