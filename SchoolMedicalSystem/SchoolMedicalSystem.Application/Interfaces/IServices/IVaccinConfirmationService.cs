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
        Task<VaccinConfirmationDTOResponse> UpdateVaccinConfirmationAsync(int id, VaccinConfirmationDTORequest vaccinConfirmation);
        Task<bool> DeleteVaccinConfirmationAsync(int id);
        Task<VaccinCampainPagingDTOResponse> GetAllVaccinConfirmationsWithPagingAsync(int campaignId, int pageSize, int pageNumber);
        Task<VaccinConfirmationDTOResponse?> GetVaccinConfirmationByStudentIdAsync(int studentId);
        Task<VaccinCampaignDTOResponse> GetVaccinConfirmationsByParentIdAsync(int parentId);

    }
}
