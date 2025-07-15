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
        IMedicationRepository Medications { get; }
        IGivenDoseRepository GivenDoses { get; }
        IStudentRepository Students { get; }

        IHealthCheckRepository HealthChecks { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<ITransaction?> GetCurrentTransactionAsync();
        IMedicalIncidentRepository MedicalIncidents {  get; }
        IUserRepository Users { get; }
        IMedicalSuppliesRepository MedicalSupplies { get; }
        IBatchRepository Batch { get; }
        IHealthProfileRepository HealthProfiles { get; }
        IMedicationHistoryRepository MedicationHistory { get; }
        IVaccinCampaignRepository VaccinCampaigns { get; }
        IVaccinConfirmationRepository VaccinConfirmations { get; }
        IVaccinDocumentRepository VaccinDocuments { get; }
        IBlogRepository Blogs { get; }
        IPrescriptionRepository Prescriptions { get; }
        IPrescriptionMedicineRepository PrescriptionMedicines { get; }
        IStudentParentsRepository StudentParents { get; }
        IHealthCheckConfirmationRepository HealthCheckConfirmations { get; }
        IMedicalRepository Medical { get; }
    }
}
