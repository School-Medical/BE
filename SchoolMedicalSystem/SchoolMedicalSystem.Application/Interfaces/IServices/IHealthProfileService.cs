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
    public interface IHealthProfileService
    {
        Task<List<HealthProfileDTOResponse>> GetAllAsync();
        Task<HealthProfile?> GetByIdAsync(int id);

        Task<HealthProfile?> GetByStudentNameAsync(string studentName);

        Task<HealthProfileDTORequest> AddAsync(HealthProfileDTORequest healthProfile);

        Task<bool> UpdateAsync(HealthProfile entity);

        Task<bool> DeleteAsync(int id);
    }
}
