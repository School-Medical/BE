using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IHealthCheckConfirmationRepository : IGenericRepository<HealthCheckConfirmation>
    {
        Task<HealthCheckConfirmation?> GetByHealthCheckAndStudentAsync(int healthCheckId, int studentId);
        Task<IEnumerable<HealthCheckConfirmation>> GetByHealthCheckIdAsync(int healthCheckId);
        Task<IEnumerable<HealthCheckConfirmation>> GetByParentIdAsync(int parentId);
        Task<IEnumerable<HealthCheckConfirmation>> GetByStatusAsync(ulong status);
        

    }
}
