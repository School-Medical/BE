using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Domain.Entities;
using SchoolMedicalSystem.Infrastructure.Data;

namespace SchoolMedicalSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly SchoolMedicalDbContext _context;
        private IDbContextTransaction? _transaction;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(SchoolMedicalDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;

            // Repository initialization using Dependency Injection
            MedicalIncidents = new MedicalIncidentRepository(_context);
            Medications = new MedicationRepository(_context);
            GivenDoses = new GivenDoseRepository(_context);
            Users = new UserRepository(_context);
            MedicalSupplies = new MedicalSuppliesRepository(_context);
            Batch = new BatchRepository(_context);
            Students = new StudentRepository(_context);
            HealthProfiles = new HealthProfileRepository(_context);
            VaccinCampaigns = new VaccinCampaignRepository(_context);
            VaccinConfirmations = new VaccinConfirmationRepository(_context);
            VaccinDocuments = new VaccinDocumentRepository(_context);
            Blogs = new BlogRepository(_context);
            Prescriptions = new PrescriptionRepository(_context);
            PrescriptionMedicines = new PrescriptionMedicineRepository(_context);
            StudentParents = new StudentParentsRepository(_context);
            HealthCheckConfirmations = new HealthCheckConfirmationRepository(_context);
            MedicationHistory = new MedicationHistoryRepository(_context);
            Medical = new MedicalRepository(_context);
            HealthChecks = new HealthCheckRepository(_context);
            StudentParent = new StudentParentRepository(_context);
            HealthCheckDocuments = new HealthCheckDocumentRepository(_context);
        }

        public IMedicalIncidentRepository MedicalIncidents { get; private set; }

        public IMedicationRepository Medications { get; private set; }

        public IGivenDoseRepository GivenDoses { get; private set; }

        public IStudentRepository Students { get; private set; }

        public IHealthCheckRepository HealthChecks { get; private set; }

        public IHealthCheckDocumentRepository HealthCheckDocuments { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
                return;

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                _transaction?.Commit();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public Task<ITransaction?> GetCurrentTransactionAsync()
        {
            if (_transaction == null)
                return Task.FromResult<ITransaction?>(null);

            return Task.FromResult<ITransaction?>(new EfTransaction(_transaction));
        }
        //This place to start progress dependency injection
        public IUserRepository Users { get; private set; }
        public IMedicalSuppliesRepository MedicalSupplies { get; private set; }
        public IBatchRepository Batch { get; private set; }
        public IHealthProfileRepository HealthProfiles { get; private set; }
        public IMedicationHistoryRepository MedicationHistory { get; private set; }
        public IVaccinCampaignRepository VaccinCampaigns { get; private set; }

        public IVaccinConfirmationRepository VaccinConfirmations { get; private set; }

        public IVaccinDocumentRepository VaccinDocuments { get; private set; }

        public IBlogRepository Blogs { get; private set; }

        public IPrescriptionRepository Prescriptions { get; private set; }
        public IPrescriptionMedicineRepository PrescriptionMedicines { get; private set; }
        public IStudentParentsRepository StudentParents { get; private set; }
        public IHealthCheckConfirmationRepository HealthCheckConfirmations { get; private set; }
        public IMedicalRepository Medical { get; private set; }
        public IStudentParentRepository StudentParent { get; private set; }
    }
}
