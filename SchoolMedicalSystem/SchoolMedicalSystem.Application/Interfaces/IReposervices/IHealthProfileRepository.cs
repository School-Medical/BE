using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IHealthProfileRepository
    {
        Task<List<HealthProfile>> GetAllAsync();
        Task<List<HealthProfile>> GetPagedAsync(int pageSize, int pageNumber);
        Task<HealthProfile?> GetByIdAsync(int id);
        Task<HealthProfile> AddAsync(HealthProfile entity);
        Task<bool> UpdateAsync(HealthProfile entity);
        Task<bool> DeleteAsync(int id);
        Task<int> CountAsync();
        Task<HealthProfile?> GetByStudentIdAsync(int studentId);

    }
}
