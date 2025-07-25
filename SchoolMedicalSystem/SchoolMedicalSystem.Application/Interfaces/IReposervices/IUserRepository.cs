using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IUserRepository
    {
        Task<User?> GetByAccountAsync(string account);
        Task<User?> CreatedAccountAsync(User user);
        Task<User?> GetByIdAsync(int userId);
        Task<(List<User> users, int totalItems)> GetPagedAsync(int pageSize, int pageNumber);
        Task<User> UpdateAsync(User user);
        Task<List<User>> GetNursesAsync();
    }
}
