using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IVaccinCampaignService
    {
        Task<IEnumerable<VaccinCampaignDTOResponse>> GetAllVaccinCampaignsAsync();
        Task<VaccinCampaignDTOResponse?> GetVaccinCampaignByIdAsync(int id);
        Task<VaccinCampaignDTOResponse> CreateVaccinCampaignAsync(VaccinCampaignDTORequest vaccinCampaign);
        Task<bool> UpdateVaccinCampaignAsync(int id,VaccinCampaignDTORequest vaccinCampaign);
        Task<bool> DeleteVaccinCampaignAsync(int id);
    }
}
