using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Domain.Entities;
using SchoolMedicalSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            MedicalSupplies = new MedicalSuppliesRepository(_context);
            Batch = new BatchRepository(_context);
            Students = new StudentRepository(_context);
        }

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
        public IMedicalIncidentRepository MedicalIncidents { get; private set; }
        public IMedicalSuppliesRepository MedicalSupplies { get; private set; }
        public IBatchRepository Batch { get; private set; }
        public IStudentRepository Students { get; private set; }
    }
}
