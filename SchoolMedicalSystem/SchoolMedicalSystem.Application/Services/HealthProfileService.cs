using AutoMapper;
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

        public HealthProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<HealthProfileDTORequest> AddAsync(HealthProfileDTORequest healthProfileDTO)
        {
           var result = await _unitOfWork.HealthProfiles.AddAsync(_mapper.Map<HealthProfile>(healthProfileDTO));
            await _unitOfWork.SaveChangesAsync();
            return healthProfileDTO;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result= _unitOfWork.HealthProfiles.GetByIdAsync(id);
            if (result != null)
            {
                await _unitOfWork.HealthProfiles.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<HealthProfileDTOResponse>> GetAllAsync()
        {
            return _mapper.Map<List<HealthProfileDTOResponse>>(await _unitOfWork.HealthProfiles.GetAllAsync());
        }

        public async Task<HealthProfile?> GetByIdAsync(int id)
        {
            return await _unitOfWork.HealthProfiles.GetByIdAsync(id);
        }

        public Task<HealthProfile?> GetByStudentNameAsync(string studentName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(HealthProfile entity)
        {
            var result = await _unitOfWork.HealthProfiles.UpdateAsync(entity);
          await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
