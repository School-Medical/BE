using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IHealthCheckDocumentRepository : IGenericRepository<HealthCheckDocument>
    {
        Task<IEnumerable<HealthCheckDocument>> GetDocumentsByHealthCheckIdAsync(int healthCheckId);
        Task<IEnumerable<HealthCheckDocument>> GetDocumentsByConfirmationIdAsync(int confirmationId);
        Task<HealthCheckDocument?> GetDocumentByStudentAndHealthCheckIdAsync(int studentId, int healthCheckId);
    }
}
