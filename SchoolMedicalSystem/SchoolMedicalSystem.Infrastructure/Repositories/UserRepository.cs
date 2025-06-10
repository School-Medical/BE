using Microsoft.EntityFrameworkCore;
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
    public class UserRepository : IUserRepository
    {
        private readonly SchoolMedicalDbContext _dbContext;
        public UserRepository(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByAccountAsync(string account)
        {
            var list = await _dbContext.Users.Select(u => u.account).ToListAsync();
            var result = await _dbContext.Users
                .Include(u => u.role)
                .FirstOrDefaultAsync(u => u.account.Equals(account));
            return result;
        }
    }
}
