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
    public class HealthCheckConfirmationRepository : GenericRepository<HealthCheckConfirmation>, IHealthCheckConfirmationRepository
    {
        public HealthCheckConfirmationRepository(SchoolMedicalDbContext context) : base(context)
        {
        }

        public async Task<HealthCheckConfirmation?> GetByHealthCheckAndStudentAsync(int healthCheckId, int studentId)
        {
            return await _dbSet
                .Include(c => c.student)
                .Include(c => c.parent)
                .Include(c => c.health_check)
                .FirstOrDefaultAsync(c => c.health_check_id == healthCheckId && c.student_id == studentId);
        }

        public async Task<IEnumerable<HealthCheckConfirmation>> GetByHealthCheckIdAsync(int healthCheckId)
        {
            return await _dbSet
                .Include(c => c.student)
                .Include(c => c.parent)
                .Include(c => c.health_check)
                .Where(c => c.health_check_id == healthCheckId)
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthCheckConfirmation>> GetByParentIdAsync(int parentId)
        {
            return await _dbSet
                .Include(c => c.student)
                .Include(c => c.parent)
                .Include(c => c.health_check)
                .Where(c => c.parent_id == parentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthCheckConfirmation>> GetByStatusAsync(ulong status)
        {
            return await _dbSet
                .Include(c => c.student)
                .Include(c => c.parent)
                .Include(c => c.health_check)
                .Where(c => c.status == status)
                .ToListAsync();
        }
    }
}
