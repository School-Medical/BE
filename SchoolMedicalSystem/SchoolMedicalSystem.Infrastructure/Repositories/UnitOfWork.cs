using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
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
            MedicalIncidents = new MedicalIncidentRepository(_context);
            Medication = new MedicationRepository(_context);
        }

        public IMedicalIncidentRepository MedicalIncidents { get; private set; }

        public IMedicationRepository Medication { get; private set; }

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

    }
}
