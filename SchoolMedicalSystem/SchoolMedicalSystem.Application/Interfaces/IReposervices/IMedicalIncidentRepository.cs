using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IMedicalIncidentRepository
    {
        Task<List<MedicalIncident> > GetAllAsync();
        Task<MedicalIncident?> GetByIdAsync(int id);
        Task<MedicalIncident> AddAsync(MedicalIncident entity);
        Task<bool> UpdateAsync(MedicalIncident entity);
        Task<bool> DeleteAsync(int id);
    }
}
