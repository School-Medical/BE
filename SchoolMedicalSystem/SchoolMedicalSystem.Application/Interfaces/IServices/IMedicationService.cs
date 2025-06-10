using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IMedicationService
    {
        Task<List<MedicationResponse>> GetAllAsync();

        Task<MedicationResponse> GetByNameAsync(string id);

        Task<MedicationResponse> AddAsync(MedicationRequest request);

        Task<MedicationResponse> UpdateAsync(MedicationRequest request);

        Task<MedicationResponse> DeleteAsync(int id);
    }
}
