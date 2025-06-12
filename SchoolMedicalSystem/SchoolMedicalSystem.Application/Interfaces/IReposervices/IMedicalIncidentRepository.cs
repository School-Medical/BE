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
        //IQueryable<MedicalIncident> GetPagedAsyncGetAll();
        Task<List<MedicalIncident>> GetPagedAsync(int pageSize, int pageNumber);
        Task<MedicalIncident?> GetByIdAsync(int id);
        Task<MedicalIncident?> GetByStudentIdAsync(int studentId);
        Task<MedicalIncident> AddAsync(MedicalIncident entity);
        Task<MedicalIncident> UpdateAsync(MedicalIncident entity);
        Task<bool> DeleteAsync(int id);
    }
}
