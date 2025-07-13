using Microsoft.EntityFrameworkCore;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Domain.Entities;
using SchoolMedicalSystem.Infrastructure.Data;

namespace SchoolMedicalSystem.Infrastructure.Repositories
{
    public class GivenDoseRepository : GenericRepository<GivenDose>, IGivenDoseRepository
    {
        public GivenDoseRepository(SchoolMedicalDbContext context) : base(context)
        {
        }
        
        public override async Task<IEnumerable<GivenDose>> GetAllAsync()
        {
            return await _context.GivenDoses
                        .Include(g => g.student)
                        .Include(g => g.parent)
                        .Include(g => g.nurse)
                        .ToListAsync();
        }

        public override async Task<GivenDose?> GetByIdAsync(int id)
        {
            return await _context.GivenDoses.Include(g => g.student)
                        .Include(g => g.parent)
                        .Include(g => g.nurse).FirstOrDefaultAsync(g => g.given_dose_id.Equals(id));
        }

        public async Task<GivenDose?> GetGivenDoseByStudentId(int studentId)
        {
            return await _context.GivenDoses.Include(g => g.student)
        .Include(g => g.parent)
        .Include(g => g.nurse).FirstOrDefaultAsync(s => s.student_id.Equals(studentId));
        }

        public async Task<List<GivenDose>> GetByParentId(int parentId)
        {
            return await _context.GivenDoses
                .Where(g => g.parent_id == parentId)
                .Include(g => g.student)
                .Include(g => g.parent)
                .Include(g => g.nurse)
                .ToListAsync();
        }
    }
}
