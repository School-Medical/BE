using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IVaccinConfirmationService
    {
        Task<IEnumerable<VaccinConfirmationDTOResponse>> GetAllVaccinConfirmationsAsync();
        Task<VaccinConfirmationDTOResponse?> GetVaccinConfirmationByIdAsync(int id);
        Task<VaccinConfirmationDTOResponse> CreateVaccinConfirmationAsync(int parentId, VaccinConfirmationDTORequest vaccinConfirmation);
        Task<bool> UpdateVaccinConfirmationAsync(int id, VaccinConfirmationDTORequest vaccinConfirmation);
        Task<bool> DeleteVaccinConfirmationAsync(int id);
        Task<PaginatedResponse<VaccinConfirmationDTOResponse>> GetAllVaccinConfirmationsWithPagingAsync(int pageSize, int pageNumber);
        Task<VaccinConfirmationDTOResponse?> GetVaccinConfirmationByStudentIdAsync(int studentId);
    }
}
