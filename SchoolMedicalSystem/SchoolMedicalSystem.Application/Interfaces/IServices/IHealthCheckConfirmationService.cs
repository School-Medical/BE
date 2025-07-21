using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IHealthCheckConfirmationService
    {
        Task<IEnumerable<HealthCheckConfirmationResponse>> GetAllHealthCheckConfirmationsAsync();
        Task<HealthCheckConfirmationResponse?> GetHealthCheckConfirmationByIdAsync(int id);
        Task<HealthCheckConfirmationResponse> CreateHealthCheckConfirmationAsync(int parentId, HealthCheckConfirmationRequest healthCheckConfirmation);
        Task<HealthCheckConfirmationResponse> UpdateHealthCheckConfirmationAsync(int id, HealthCheckConfirmationRequest healthCheckConfirmation);
        Task<bool> DeleteHealthCheckConfirmationAsync(int id);
        Task<HealthCheckConfirmationResponse?> GetHealthCheckConfirmationByStudentIdAsync(int studentId, int healthCheckId);
        Task<IEnumerable<HealthCheckConfirmationResponse>> GetHealthCheckConfirmationsByParentIdAsync(int parentId);
        Task<IEnumerable<HealthCheckConfirmationResponse>> GetHealthCheckConfirmationsByHealthCheckIdAsync(int healthCheckId);
        Task<IEnumerable<HealthCheckConfirmationResponse>> GetHealthCheckConfirmationsByStatusAsync(int status);
    }
}
