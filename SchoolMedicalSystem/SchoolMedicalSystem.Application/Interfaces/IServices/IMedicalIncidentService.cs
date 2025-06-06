using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IMedicalIncidentService
    {
        Task<List<MedicalIncident>> GetAllAsync();
        Task<MedicalIncident?> GetByIdAsync(int id);
        Task<MedicalIncident?> GetByStudentNameAsync(string studentName);
        Task<MedicalIncidentDTORequest> AddAsync(MedicalIncidentDTORequest medicalIncidentDTO);
        Task<bool> UpdateAsync(MedicalIncident entity);
        Task<bool> DeleteAsync(int id);
    }
}
