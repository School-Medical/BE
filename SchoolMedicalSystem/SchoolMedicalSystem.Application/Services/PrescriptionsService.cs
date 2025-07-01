using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Domain.Entities;

namespace SchoolMedicalSystem.Application.Services
{
    public class PrescriptionsService : IPrescriptionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PrescriptionsService> _logger;

        public PrescriptionsService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PrescriptionsService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PrescriptionDTORespone> AddAsync(PrescriptionDTORequest entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogWarning("Attempted to add a null prescription");
                    throw new ArgumentNullException(nameof(entity), "Prescription cannot be null");
                }

                var prescription = _mapper.Map<Prescription>(entity);
                var added = await _unitOfWork.Prescriptions.AddAsync(prescription);

                // Save changes if not handled in repository
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<PrescriptionDTORespone>(added);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding prescription");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await _unitOfWork.Prescriptions.DeleteAsync(id);
                if (result)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                _logger.LogWarning("Prescription with ID {Id} not found for deletion", id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting prescription with ID: {Id}", id);
                throw;
            }
        }

        public async Task<PrescriptionDTORespone?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.Prescriptions.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Prescription with ID {Id} not found", id);
                    return null;
                }
                return _mapper.Map<PrescriptionDTORespone>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting prescription with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(PrescriptionDTORequest entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogWarning("Attempted to update a null prescription");
                    throw new ArgumentNullException(nameof(entity), "Prescription cannot be null");
                }

                var prescription = _mapper.Map<Prescription>(entity);
                var result = await _unitOfWork.Prescriptions.UpdateAsync(prescription);
                if (result)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                _logger.LogWarning("Prescription with ID {Id} not found for update", entity.PrescriptionId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating prescription with ID: {Id}", entity.PrescriptionId);
                throw;
            }
        }
    }

}
