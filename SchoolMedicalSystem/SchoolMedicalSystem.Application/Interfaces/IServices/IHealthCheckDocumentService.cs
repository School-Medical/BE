using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IHealthCheckDocumentService
    {
        Task<IEnumerable<HealthCheckDocumentResponse>> GetAllHealthCheckDocumentsAsync();
        Task<HealthCheckDocumentResponse> GetHealthCheckDocumentByIdAsync(int id);
        Task<HealthCheckDocumentResponse> CreateHealthCheckDocumentAsync(HealthCheckDocumentRequest healthCheckDocument);
        Task<HealthCheckDocumentResponse> UpdateHealthCheckDocumentAsync(int id, HealthCheckDocumentRequest healthCheckDocument);
        Task<bool> DeleteHealthCheckDocumentAsync(int id);
        Task<IEnumerable<HealthCheckDocumentResponse>> GetDocumentsByHealthCheckIdAsync(int healthCheckId);
        Task<IEnumerable<HealthCheckDocumentResponse>> GetDocumentsByConfirmationIdAsync(int confirmationId);
        Task<HealthCheckConfirmationResponse> GetDocumentByStudentAndHealthCheckId(int studentId, int healthCheckId);
    }
}
