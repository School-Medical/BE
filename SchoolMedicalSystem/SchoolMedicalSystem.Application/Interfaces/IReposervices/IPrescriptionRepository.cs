using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IPrescriptionRepository
    {
        Task<List<Prescription>> GetAllAsync();
        Task<List<Prescription>> GetPagedAsync(int pageSize, int pageNumber);
        Task<Prescription?> GetByIdAsync(int id);
        Task<Prescription> AddAsync(Prescription entity);
        Task<bool> UpdateAsync(Prescription entity);
        Task<bool> DeleteAsync(int id);
        Task<int> CountAsync();
        Task<Prescription?> GetByMedicalIncidentIdAsync(int incidentId);
        Task<int> DeleteByMedicalIncidentIdAsync(int medicalIncidentId);
    }
}
