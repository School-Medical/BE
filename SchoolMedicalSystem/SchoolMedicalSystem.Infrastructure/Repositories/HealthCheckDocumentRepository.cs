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
    public class HealthCheckDocumentRepository : GenericRepository<HealthCheckDocument>, IHealthCheckDocumentRepository
    {
        public HealthCheckDocumentRepository(SchoolMedicalDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<HealthCheckDocument>> GetDocumentsByHealthCheckIdAsync(int healthCheckId)
        {
            return await _context.HealthCheckDocuments
                .Where(doc => doc.health_check_id == healthCheckId)
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthCheckDocument>> GetDocumentsByConfirmationIdAsync(int confirmationId)
        {
            return await _context.HealthCheckDocuments
                .Where(doc => doc.health_check_confirm_id == confirmationId)
                .ToListAsync();
        }

        public async Task<HealthCheckDocument?> GetDocumentByStudentAndHealthCheckIdAsync(int studentId, int healthCheckId)
        {
            return await _context.HealthCheckDocuments
                .FirstOrDefaultAsync(doc => doc.student_id == studentId && doc.health_check_id == healthCheckId);
        }
    }
}
