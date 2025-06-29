using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IVaccinConfirmationRepository
    {
        Task<VaccinConfirmation> CreateAsync(VaccinConfirmation campaign);
        Task<VaccinConfirmation?> GetByIdAsync(int id);
        Task<IEnumerable<VaccinConfirmation>> GetAllAsync();
        Task<VaccinConfirmation> UpdateAsync(VaccinConfirmation confirmation);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<VaccinConfirmation>> GetAllWithPagingAsync(int pageSize, int pageNumber);
        Task<int> CountAsync();
        Task<VaccinConfirmation?> GetVaccinConfirmationByStudentIdAsync(int studentId);
        Task<VaccinConfirmation?> GetVaccinConfirmationByParentIdAsync(int parentId);
        Task<VaccinConfirmation?> GetVaccinConfirmationByStudentAndCampaignIdAsync(int studentId, int campaignId);
    }
}
