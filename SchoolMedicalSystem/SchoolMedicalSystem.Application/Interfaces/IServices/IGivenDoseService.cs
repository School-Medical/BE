using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IGivenDoseService
    {
        Task<GivenDoseResponse> AddAsync(GivenDoseRequest request);
        Task<GivenDoseResponse> UpdateAsync(int id, GivenDoseRequest request);
        Task<List<GivenDoseResponse>> SearchByStudentNameAsync(string studentName);
        Task<List<GivenDoseResponse>> GetAllAsync();
        Task<GivenDoseResponse?> GetByIdAsync(int id);
        Task<List<GivenDoseResponse>> GetByParentId(int id);
    }
}
