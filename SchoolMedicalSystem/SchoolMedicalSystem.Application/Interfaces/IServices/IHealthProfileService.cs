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
        Task<PaginatedResponse<HealthProfileDTOResponse>> GetAllAsync(int pageSize, int pageNumber);
        Task<HealthProfileDTOResponse?> GetByIdAsync(int id);

        Task<HealthProfile?> GetByStudentNameAsync(string studentName);

        Task<HealthProfileDTOResponse> AddAsync(HealthProfileDTORequest dto);

        Task<HealthProfileDTOResponse> UpdateAsync(int healthProfileId, HealthProfileDTORequest dto);

        Task<bool> DeleteAsync(int id);
    }
}
