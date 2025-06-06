using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Application.Mappers;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Services
{
    public class MedicalIncidentService : IMedicalIncidentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalIncidentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MedicalIncidentDTORequest> AddAsync(MedicalIncidentDTORequest medicalIncidentDTO)
        {
            var result = await _unitOfWork.MedicalIncidents.AddAsync(_mapper.Map<MedicalIncident>(medicalIncidentDTO));
            await _unitOfWork.SaveChangesAsync();
            return medicalIncidentDTO;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _unitOfWork.MedicalIncidents.GetByIdAsync(id);
            if (result != null)
            {
                await _unitOfWork.MedicalIncidents.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<MedicalIncident>> GetAllAsync()
        {
            return await _unitOfWork.MedicalIncidents.GetAllAsync();
        }

        public async Task<MedicalIncident?> GetByIdAsync(int id)
        {
            return await _unitOfWork.MedicalIncidents.GetByIdAsync(id);
        }

        public Task<MedicalIncident?> GetByStudentNameAsync(string studentName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(MedicalIncident entity)
        {
            var result = await _unitOfWork.MedicalIncidents.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
