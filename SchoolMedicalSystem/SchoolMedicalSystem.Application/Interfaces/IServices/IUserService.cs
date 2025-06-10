using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<User?> ValidateUserAsync(string account, string password);
        string GenerateJwtToken(User user);
    }
}
