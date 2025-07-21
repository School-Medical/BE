using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IHealthCheckService
    {
        Task<IEnumerable<HealthCheckResponse>> GetAllHealthChecksAsync();
        Task<HealthCheckResponse?> GetHealthCheckByIdAsync(int id);
        Task<HealthCheckResponse> CreateHealthCheckAsync(HealthCheckRequest healthCheck);
        Task<HealthCheckResponse> UpdateHealthCheckAsync(int id, HealthCheckRequest healthCheck);
        Task<PaginatedResponse<HealthCheckResponse>> GetHealthChecksPaginatedAsync(int pageSize, int pageNumber);
        Task<IEnumerable<HealthCheckResponse>> GetActiveHealthChecksAsync();
        Task<IEnumerable<HealthCheckResponse>> GetHealthChecksByStatusAsync(int status);

    }
}
