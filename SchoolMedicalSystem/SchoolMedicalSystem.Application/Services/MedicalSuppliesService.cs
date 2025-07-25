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
    public class MedicalSuppliesService : IMedicalSuppliesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MedicalSuppliesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MedicalSuppliesDTOResponse> AddAsync(MedicalSuppliesDTORequest dto, int userId)
        {
            var batch = new Batch
            {
                user_id = userId,
                import_date = DateTime.Now
            };
            var batchResult = await _unitOfWork.Batch.AddBatchAsync(batch);
            await _unitOfWork.SaveChangesAsync();
            if( batchResult == null)
            {
                throw new Exception("Failed to create batch for the medical supply.");
            }

            var mappedEntity = _mapper.Map<Medicine>(dto);
            mappedEntity.batch_id = batchResult.batch_id;
            var result = await _unitOfWork.MedicalSupplies.AddAsync(mappedEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<MedicalSuppliesDTOResponse>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await _unitOfWork.MedicalSupplies.GetByIdAsync(id);

            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Medical supply with ID {id} not found.");
            }

            var result = await _unitOfWork.MedicalSupplies.DeleteAsync(id);

            if (result && existingEntity.batch_id.HasValue)
            {
                await _unitOfWork.Batch.DeleteBatchAsync(existingEntity.batch_id.Value);
            }

            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<List<MedicalSuppliesDTOResponse>> GetAllAsync()
        {
            var result = await _unitOfWork.MedicalSupplies.GetAllasync();
            var mapper = _mapper.Map<List<MedicalSuppliesDTOResponse>>(result);
            return mapper;
        }

        public async Task<MedicalSuppliesDTOResponse> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.MedicalSupplies.GetByIdAsync(id);
            var mapper = _mapper.Map<MedicalSuppliesDTOResponse>(result);
            return mapper;
        }

        public Task<List<Medicine>> GetMedicinesByCategoryAsync(string category)
        {
            var result = _unitOfWork.MedicalSupplies.GetMedicinesByCategoryAsync(category);
            return result;
        }

        public async Task<MedicalSuppliesDTOResponse> UpdateAsync(int id, MedicalSuppliesDTORequest dto)
        {
            var existingEntity = await _unitOfWork.MedicalSupplies.GetByIdAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Medical supply with ID {id} not found.");
            }
            _mapper.Map(dto, existingEntity);
            var result = await _unitOfWork.MedicalSupplies.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<MedicalSuppliesDTOResponse>(result);
        }
    }
}
