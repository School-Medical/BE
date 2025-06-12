using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolMedicalSystem.Application.DTO.Request;
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
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<UserLoginDTOResponse?> ValidateUserAsync(string account, string password)
        {
            var user = await _unitOfWork.Users.GetByAccountAsync(account);
            if (user == null) return null;

            //var result = (user.hash_password == password) ? user : null;
            //return _mapper.Map<UserLoginDTOResponse>(result);
            return VerifyPassword(password, user.hash_password) ? _mapper.Map<UserLoginDTOResponse>(user) : null;
        }

        public async Task<UserRegisterDTOResponse?> CreatedAccountAsync(UserRegisterDTORequest user)
        {
            var userEntity = _mapper.Map<User>(user);
            userEntity.hash_password = EncryptPassword(user.HashPassword);
            var result = await _unitOfWork.Users.CreatedAccountAsync(userEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserRegisterDTOResponse>(result) ;
        }

        public string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword) ? true : false;

        }

    }
    }
