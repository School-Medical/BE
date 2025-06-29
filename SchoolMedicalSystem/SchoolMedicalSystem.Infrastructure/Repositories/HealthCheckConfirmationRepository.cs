using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Infrastructure.Repositories
{
    public class HealthCheckConfirmationRepository : IHealthCheckConfirmationRepository
    {
        private readonly SchoolMedicalDbContext _context;
        public HealthCheckConfirmationRepository(SchoolMedicalDbContext context)
        {
            _context = context;
        }
    }
}
