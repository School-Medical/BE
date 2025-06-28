using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IVaccinDocumentRepository
    {
        Task<VaccinDocument> CreateAsync(VaccinDocument campaign);
        Task<VaccinDocument?> GetByIdAsync(int id);
        Task<IEnumerable<VaccinDocument>> GetAllAsync();
        Task<bool> UpdateAsync(VaccinDocument campaign);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<VaccinDocument>> GetAllWithPagingAsync(int pageSize, int pageNumber);
        Task<int> CountAsync();
        Task<VaccinDocument?> GetVaccinDocumentByStudentIdAsync(int studentId);
    }
}
