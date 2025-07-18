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

        public async Task<User?> CreatedAccountAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            return user;
        }

        public async Task<User?> GetByAccountAsync(string account)
        {
            var list = await _dbContext.Users.Select(u => u.account).ToListAsync();
            var result = await _dbContext.Users
                .Include(u => u.role)
                .FirstOrDefaultAsync(u => u.account.Equals(account));
            return result;
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            var result = await _dbContext.Users
                .Include(u => u.role)
                .Include(u => u.Students)
                .Include(u => u.StudentParents)
                .ThenInclude(u => u.student)
                .FirstOrDefaultAsync(u => u.user_id == userId);
            return result;
        }

        public async Task<(List<User> users, int totalItems)> GetPagedAsync(int pageSize, int pageNumber)
        {
            var query = _dbContext.Users
                .Include(u => u.role)
                .Include(u => u.Students)
                .Include(u => u.StudentParents)
                .ThenInclude(u => u.student);

            var totalItems = await query.CountAsync();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalItems);
        }


        public async Task<User> UpdateAsync(User user)
        {
            _dbContext.Update(user);
            return user;
        }
    }
}
