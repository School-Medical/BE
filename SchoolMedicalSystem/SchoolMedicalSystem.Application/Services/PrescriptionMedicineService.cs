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
    public class PrescriptionMedicineService : IPrescriptionMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PrescriptionMedicineService> _logger;

        public PrescriptionMedicineService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PrescriptionMedicineService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PrescriptionMedicineDTORespone> AddAsync(PrescriptionMedicineDTORequest entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var prescriptionMedicine = _mapper.Map<PrescriptionMedicine>(entity);
                var added = await _unitOfWork.PrescriptionMedicines.AddAsync(prescriptionMedicine);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<PrescriptionMedicineDTORespone>(added);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding PrescriptionMedicine");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await _unitOfWork.PrescriptionMedicines.DeleteAsync(id);
                if (result)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                _logger.LogWarning("PrescriptionMedicine with ID {Id} not found for deletion", id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting PrescriptionMedicine with ID: {Id}", id);
                throw;
            }
        }

        public async Task<PrescriptionMedicineDTORespone> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.PrescriptionMedicines.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("PrescriptionMedicine with ID {Id} not found", id);
                    return null;
                }
                return _mapper.Map<PrescriptionMedicineDTORespone>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting PrescriptionMedicine with ID: {Id}", id);
                throw;
            }
        }

        public async Task<List<PrescriptionMedicineDTORespone>> GetByPrescriptionId(int prescriptionId)
        {
            try
            {
                var entities = await _unitOfWork.PrescriptionMedicines.GetByPrescriptionId(prescriptionId);
                return _mapper.Map<List<PrescriptionMedicineDTORespone>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting PrescriptionMedicines by PrescriptionId: {Id}", prescriptionId);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(PrescriptionMedicineDTORequest entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var prescriptionMedicine = _mapper.Map<PrescriptionMedicine>(entity);
                var result = await _unitOfWork.PrescriptionMedicines.UpdateAsync(prescriptionMedicine);
                if (result)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                _logger.LogWarning("PrescriptionMedicine with ID {Id} not found for update", entity.PrescriptionMedicineId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating PrescriptionMedicine with ID: {Id}", entity.PrescriptionMedicineId);
                throw;
            }
        }
    }
}
