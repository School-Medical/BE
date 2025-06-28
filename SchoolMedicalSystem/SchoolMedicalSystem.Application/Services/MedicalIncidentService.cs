using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
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
        private readonly ILogger<MedicalIncidentService> _logger;

        public MedicalIncidentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<MedicalIncidentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MedicalIncidentDTOResponse> AddAsync(MedicalIncidentDTORequest medicalIncidentDTO)
        {
            try
            {
                if (medicalIncidentDTO == null) throw new ArgumentNullException(nameof(medicalIncidentDTO));

                var entity = await _unitOfWork.MedicalIncidents.AddAsync(_mapper.Map<MedicalIncident>(medicalIncidentDTO));
                await _unitOfWork.SaveChangesAsync();

                var result = _mapper.Map<MedicalIncidentDTOResponse>(entity);
                //_logger.LogInformation("Successfully created MedicalIncident with ID: {Id}", entity.medical_incident_id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating MedicalIncident");
                throw;
            }
        }


        public async Task<bool> DeleteAsync(int medicalIncidentID)
        {
            try
            {
                // Lấy đơn thuốc của sự cố y tế
                var prescription = await _unitOfWork.Prescriptions.GetByMedicalIncidentIdAsync(medicalIncidentID);
                if (prescription != null)
                {
                    // Xóa tất cả thuốc trong đơn
                    await _unitOfWork.PrescriptionMedicines.DeleteByPrescriptionIdAsync(prescription.prescription_id);

                    // Xóa đơn thuốc
                    await _unitOfWork.Prescriptions.DeleteByMedicalIncidentIdAsync(medicalIncidentID);
                }

                // Xóa medical incident
                var result = await _unitOfWork.MedicalIncidents.DeleteAsync(medicalIncidentID);
                if (result)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }

                _logger.LogWarning("MedicalIncident with ID: {Id} not found for deletion", medicalIncidentID);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting MedicalIncident with ID: {Id}", medicalIncidentID);
                throw;
            }
        }

        public async Task<PaginatedResponse<MedicalIncidentDTOResponse>> GetAllAsync(int pageSize, int pageNumber)
        {

            int totalItems = await _unitOfWork.MedicalIncidents.CountAsync();
            var pagedEntities = await _unitOfWork.MedicalIncidents.GetPagedAsync(pageSize, pageNumber);
            var result = _mapper.Map<List<MedicalIncidentDTOResponse>>(pagedEntities);

            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return new PaginatedResponse<MedicalIncidentDTOResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = result
            };

        }

        public async Task<MedicalIncidentDTOResponse?> GetByIdAsync(int medicalIncidentID)
        {
            try
            {
                var entity = await _unitOfWork.MedicalIncidents.GetByIdAsync(medicalIncidentID);
                return _mapper.Map<MedicalIncidentDTOResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting MedicalIncident with ID: {Id}", medicalIncidentID);
                throw;
            }
        }


        public async Task<MedicalIncidentDTOResponse?> GetByStudentCodeOrByNameAsync(string studentCode)
        {
            try
            {
                if (string.IsNullOrEmpty(studentCode) )
                {
                    _logger.LogWarning("Both studentCode and studentName are empty");
                    return null;
                }

                Student? result = null; // Dấu ? Cho phép trả null
                
                if (!string.IsNullOrEmpty(studentCode))
                {
                    result = await _unitOfWork.Students.GetStudentByStudentCode(studentCode);
                }

                if (result == null)
                    return null;

                return _mapper.Map<MedicalIncidentDTOResponse>(
                    await _unitOfWork.MedicalIncidents.GetByStudentIdAsync(result.student_id)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finding medical incident for student {Name}", studentCode);
                throw;
            }
        }


        public async Task<MedicalIncidentDTOResponse> UpdateAsync(int medicalIncidentId, MedicalIncidentDTORequest dto)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(dto));

                var entity = await _unitOfWork.MedicalIncidents.GetByIdAsync(medicalIncidentId);
                 _mapper.Map(dto, entity);

                if (entity == null)
                    throw new KeyNotFoundException($"Medical Incident with ID {medicalIncidentId} not found.");

                await _unitOfWork.MedicalIncidents.UpdateAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<MedicalIncidentDTOResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating MedicalIncident with ID: {Id}", medicalIncidentId);
                throw;
            }

        }


    }
}
