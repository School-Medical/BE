using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Services
{
    public class HealthProfileService : IHealthProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<HealthProfileService> _logger;

        public HealthProfileService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<HealthProfileService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<HealthProfileDTOResponse> AddAsync(HealthProfileDTORequest healthProfileDTO)
        {
          try
          {
              if (healthProfileDTO == null) throw new ArgumentNullException(nameof(healthProfileDTO));

              var entity = await _unitOfWork.HealthProfiles.AddAsync(_mapper.Map<HealthProfile>(healthProfileDTO));
              await _unitOfWork.SaveChangesAsync();
              var result = _mapper.Map<HealthProfileDTOResponse>(entity);
              return result;
          }
          catch (Exception ex)
          {
              _logger.LogError(ex, "Error creating HealthProfile");
              throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
           try 
           {
               var result = await _unitOfWork.HealthProfiles.DeleteAsync(id);
               if (result)
               {
                   await _unitOfWork.SaveChangesAsync();
                   return true;
               }
               _logger.LogWarning("HealthProfile with ID: {Id} not found for deletion", id);
                return false;
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, "Error deleting HealthProfile with ID: {Id}", id);
               throw;
            }
        }

        public async Task<PaginatedResponse<HealthProfileDTOResponse>> GetAllAsync(int pageSize, int pageNumber)
        {
            try
                {
                var healthProfiles = await _unitOfWork.HealthProfiles.GetPagedAsync(pageSize, pageNumber);
                var result = _mapper.Map<List<HealthProfileDTOResponse>>(healthProfiles);

                var totalPages = (int)Math.Ceiling(result.Count / (double)pageSize);

                return new PaginatedResponse<HealthProfileDTOResponse>
                {
                    Items = result,
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    TotalItems = result.Count,
                    TotalPages = totalPages,
                    HasPreviousPage = pageNumber > 1,
                    HasNextPage = pageNumber < totalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all HealthProfiles");
                throw;
            }
        }

        public async Task<HealthProfileDTOResponse?> GetByIdAsync(int id)
        {
            try
                {              
                var healthProfile = await _unitOfWork.HealthProfiles.GetByIdAsync(id);
                if (healthProfile == null)
                {
                    _logger.LogWarning("HealthProfile with ID: {Id} not found", id);
                    return null;
                }
                return _mapper.Map<HealthProfileDTOResponse>(healthProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving HealthProfile with ID: {Id}", id);
                throw;
            }
        }

        public Task<HealthProfile?> GetByStudentNameAsync(string studentName)
        {
            throw new NotImplementedException();
        }       

        public async Task<HealthProfileDTOResponse> UpdateAsync(int healthProfileId, HealthProfileDTORequest dto)
        {
            try
                {
                if (dto == null) throw new ArgumentNullException(nameof(dto));

                var existingProfile = await _unitOfWork.HealthProfiles.GetByIdAsync(healthProfileId);
                _mapper.Map(dto, existingProfile);

                if (existingProfile == null)
                {
                    throw new KeyNotFoundException($"HealthProfile with ID {healthProfileId} not found.");
                }
                
                await _unitOfWork.HealthProfiles.UpdateAsync(existingProfile);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<HealthProfileDTOResponse>(existingProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating HealthProfile with ID: {Id}", healthProfileId);
                throw;
            }
        }

        
    }
}
