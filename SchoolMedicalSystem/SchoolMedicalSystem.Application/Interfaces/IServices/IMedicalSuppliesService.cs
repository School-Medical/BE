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
    public interface IMedicalSuppliesService
    {
        Task<List<MedicalSuppliesDTOResponse>> GetAllAsync();
        Task<MedicalSuppliesDTOResponse> GetByIdAsync(int id);
        Task<Medicine> AddAsync(MedicalSuppliesDTORequest dto, int userId);
        Task<bool> UpdateAsync(int id, MedicalSuppliesDTORequest dto);
        Task<bool> DeleteAsync(int id);
        Task<List<Medicine>> GetMedicinesByCategoryAsync(string category);
    }
}
