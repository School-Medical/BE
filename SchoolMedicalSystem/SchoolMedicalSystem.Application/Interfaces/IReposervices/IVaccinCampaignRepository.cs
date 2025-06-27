using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IVaccinCampaignRepository
    {
        Task<VaccinCampaign> CreateAsync(VaccinCampaign campaign);
        Task<VaccinCampaign?> GetByIdAsync(int id);
        Task<IEnumerable<VaccinCampaign>> GetAllAsync();
        Task<bool> UpdateAsync(VaccinCampaign campaign);
        Task<bool> DeleteAsync(int id);
    }
}
