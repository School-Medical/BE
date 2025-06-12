using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<UserLoginDTOResponse?> ValidateUserAsync(string account, string password);
        Task<UserRegisterDTOResponse?> CreatedAccountAsync(UserRegisterDTORequest user);
        bool VerifyPassword (string password, string hashPassword);
        string EncryptPassword(string password);
    }
}
