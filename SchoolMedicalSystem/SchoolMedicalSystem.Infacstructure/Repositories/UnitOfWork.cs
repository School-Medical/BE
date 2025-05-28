using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Infrastructure.Repositories
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction _transaction;
        private readonly ILogger<UnitOfWork> _logger;
    }
}
