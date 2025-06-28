using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IVaccinDocumentService
    {
        Task<IEnumerable<VaccinDocumentDTOResponse>> GetAllVaccinDocumentsAsync();
        Task<VaccinDocumentDTOResponse?> GetVaccinDocumentByIdAsync(int id);
        Task<VaccinDocumentDTOResponse> CreateVaccinDocumentAsync(VaccinDocumentDTORequest vaccinDocument);
        Task<bool> UpdateVaccinDocumentAsync(int id, VaccinDocumentDTORequest vaccinDocument);
        Task<bool> DeleteVaccinDocumentAsync(int id);
        Task<PaginatedResponse<VaccinDocumentDTOResponse>> GetAllVaccinDocumentsWithPagingAsync(int pageSize, int pageNumber);
        Task<VaccinDocument> GetVaccinDocumentByStudentIdAsync(int studentId);
    }
}
