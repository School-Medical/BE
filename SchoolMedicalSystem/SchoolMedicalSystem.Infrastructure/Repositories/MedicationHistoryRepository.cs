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
    public class MedicationHistoryRepository : IMedicationHistoryRepository
    {
        private readonly SchoolMedicalDbContext _dbContext;
        public MedicationHistoryRepository(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<GivenDose>> GetMedicationHistoryAsync(int id)
        {
            return await _dbContext.GivenDoses
                .Include(gd => gd.Medications)
                .Include(gd => gd.student)
                .Include(gd => gd.parent)
                .Include(gd => gd.nurse)
                .Where(gd => gd.student_id == id || gd.parent_id == id)
                .OrderByDescending(gd => gd.create_at)
                .ToListAsync();
        }
    }
}
