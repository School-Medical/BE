using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IUnitOfWork
    {
        IMedicationRepository Medications { get; }
        IGivenDoseRepository GivenDoses { get; }
        IStudentRepository Students { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<ITransaction?> GetCurrentTransactionAsync();
        IMedicalIncidentRepository MedicalIncidents {  get; }
    }
}
