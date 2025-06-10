using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _mapper = mapper;
        }

        public async Task<UserLoginDTOResponse?> ValidateUserAsync(string account, string password)
        {
            var user = await _unitOfWork.Users.GetByAccountAsync(account);
            if (user == null) return null;

            //return BCrypt.Net.BCrypt.Verify(password, user.hash_password) ? user : null;
            var result = (user.hash_password == password) ? user : null;
            return _mapper.Map<UserLoginDTOResponse>(result);
        }

        public string GenerateJwtToken(UserLoginDTOResponse user)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.LastName ?? ""),
            new Claim(ClaimTypes.Role, user.RoleName ?? "User")
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
