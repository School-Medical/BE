using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedResponse<UserDTOResponse>> GetAllAsync(int pageSize, int pageNumber)
        {
            var (users, totalItems) = await _unitOfWork.Users.GetPagedAsync(pageSize, pageNumber);

            var result = _mapper.Map<List<UserDTOResponse>>(users);

            var totalPage = (int)Math.Ceiling(totalItems / (double)pageSize);

            return new PaginatedResponse<UserDTOResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPage,
                Items = result,
                HasPreviousPage = pageNumber > 1,
                HasNextPage = pageNumber < totalPage
            };
        }


        public async Task<UserDTOResponse?> GetByIdAsync(int userId)
        {
            try
            {
                var entity = await _unitOfWork.Users.GetByIdAsync(userId);
                return _mapper.Map<UserDTOResponse>(entity);
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving User with ID: {Id}", userId);
                throw;
            }
        }

        public async Task<UserDTOResponse> UpdateAsync(int userId, UserDTORequest userDto)
        {
            try
            {
                if (userDto == null)
                {
                    _logger.LogWarning("User DTO is null for update operation.");
                    throw new ArgumentNullException(nameof(userDto), "User DTO cannot be null.");
                }
                var existingUser = await _unitOfWork.Users.GetByIdAsync(userId);

                if (existingUser == null)
                {
                    _logger.LogWarning("User with ID {Id} not found for update operation.", userId);
                    throw new KeyNotFoundException($"User with ID {userId} not found.");
                }

                _mapper.Map(userDto, existingUser);
                await _unitOfWork.Users.UpdateAsync(existingUser);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<UserDTOResponse>(existingUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating User with ID: {Id}", userId);
                throw;
            }
        }
    }
}
