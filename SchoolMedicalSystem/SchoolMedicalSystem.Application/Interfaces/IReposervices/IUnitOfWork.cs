using SchoolMedicalSystem.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<ITransaction?> GetCurrentTransactionAsync();
      
        IMedicalIncidentRepository MedicalIncidents {  get; }
        IUserRepository Users { get; }
        IMedicalSuppliesRepository MedicalSupplies { get; }
        IBatchRepository Batch { get; }
        IStudentRepository Students { get; }
        IHealthProfileRepository HealthProfiles { get; }
        IMedicationHistoryService MedicationHistory { get; }
    }
}
