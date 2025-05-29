using Microsoft.EntityFrameworkCore.Storage;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Infrastructure.Repositories
{
    public class EfTransaction : ITransaction
    {
        private readonly IDbContextTransaction _efTransaction;

        public EfTransaction(IDbContextTransaction efTransaction)
        {
            _efTransaction = efTransaction;
        }

        public Task CommitAsync() => _efTransaction.CommitAsync();

        public Task RollbackAsync() => _efTransaction.RollbackAsync();

        public ValueTask DisposeAsync() => _efTransaction.DisposeAsync();
    }
}
