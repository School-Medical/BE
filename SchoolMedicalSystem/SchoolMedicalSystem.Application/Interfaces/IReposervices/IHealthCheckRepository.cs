using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IHealthCheckRepository : IGenericRepository<HealthCheck>
    {
        Task<HealthCheck> CreateAsync(HealthCheck healthCheck);
        Task<int> CountAsync();
        Task<IEnumerable<HealthCheck>> GetAllWithPagingAsync(int pageSize, int pageNumber);
        Task<HealthCheck?> GetCurrentHealthCheckAsync();
        Task<IEnumerable<HealthCheck>> GetActiveHealthChecksAsync();
        Task<IEnumerable<HealthCheck>> GetHealthChecksByStatusAsync(int status);
        Task<IEnumerable<HealthCheck>> GetHealthChecksByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<HealthCheck>> GetHealthChecksByLocationAsync(string location);
        Task<bool> IsHealthCheckActiveAsync(int healthCheckId);
        Task<IEnumerable<HealthCheck>> GetUpcomingHealthChecksAsync();

    }
}
