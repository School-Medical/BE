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
                if (medicalIncidentDTO == null)
                    throw new ArgumentNullException(nameof(medicalIncidentDTO));

                // 1. Map và thêm MedicalIncident
                var medicalIncident = _mapper.Map<MedicalIncident>(medicalIncidentDTO);
                var insertedMedicalIncident = await _unitOfWork.MedicalIncidents.AddAsync(medicalIncident);
                await _unitOfWork.SaveChangesAsync();

                // 2. Map và thêm Prescription
                var prescriptionDto = medicalIncidentDTO.Prescriptions.FirstOrDefault();
                if (prescriptionDto != null)
                {
                    var prescription = _mapper.Map<Prescription>(prescriptionDto);

                    // giữ lại id để tạo
                    prescription.medical_incident_id = insertedMedicalIncident.medical_incident_id;
                    var insertedPrescription = await _unitOfWork.Prescriptions.AddAsync(prescription);
                    await _unitOfWork.SaveChangesAsync();

                    // 3. Thêm từng PrescriptionMedicine
                    foreach (var pmDto in prescriptionDto.PrescriptionMedicines)
                    {
                        var prescriptionMedicine = _mapper.Map<PrescriptionMedicine>(pmDto);
                        prescriptionMedicine.prescription_id = insertedPrescription.prescription_id; // gán ID đúng
                        await _unitOfWork.PrescriptionMedicines.AddAsync(prescriptionMedicine);
                    }

                    await _unitOfWork.SaveChangesAsync();
                }

                // 4. Map kết quả
                var result = _mapper.Map<MedicalIncidentDTOResponse>(insertedMedicalIncident);
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


        //public async Task<MedicalIncidentDTOResponse?> GetByStudentCodeOrByNameAsync(string studentCode)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(studentCode) )
        //        {
        //            _logger.LogWarning("Both studentCode and studentName are empty");
        //            return null;
        //        }

        //        Student? result = null; // Dấu ? Cho phép trả null

        //        if (!string.IsNullOrEmpty(studentCode))
        //        {
        //            result = await _unitOfWork.Students.GetStudentByStudentCode(studentCode);
        //        }

        //        if (result == null)
        //            return null;
        //        var medicalIncident = await _unitOfWork.MedicalIncidents.GetByStudentIdAsync(result.student_id);
        //        //return _mapper.Map<MedicalIncidentDTOResponse>(
        //        //    await _unitOfWork.MedicalIncidents.GetByStudentIdAsync(result.student_id)
        //        //);
        //        return new MedicalIncidentDTOResponse
        //        {
        //            MedicalIncidentId = medicalIncident!.medical_incident_id,
        //            Type = medicalIncident.type,
        //            Symptom = medicalIncident.symptom,
        //            Diagnosis = medicalIncident.diagnosis,
        //            Treatment = medicalIncident.treatment,
        //            SeverityLevel = medicalIncident.severity_level,
        //            FollowUp = medicalIncident.follow_up,
        //            Message = medicalIncident.message,
        //            CreatedAt = medicalIncident.create_at,
        //            //StudentId = result.student_id,
        //            StudentName = result.first_name + " " + result.last_name,
        //            NurseId = medicalIncident.nurse_id,
        //            NurseName = medicalIncident.nurse?.first_name + " " + medicalIncident.nurse?.last_name,
        //            //TÔi trở về map bằng tay vì tôi lười
        //            Prescriptions = medicalIncident.Prescriptions.Select(p => new PrescriptionDTORespone
        //            {
        //                PrescriptionId = p.prescription_id,
        //                Instruction = p.instruction,
        //                CreateAt = p.create_at,
        //                MedicalIncidentId = p.medical_incident_id,
        //                PrescriptionMedicines = p.PrescriptionMedicines?.Select(pm => new PrescriptionMedicineDTORespone
        //                {
        //                    PrescriptionMedicineId = pm.prescription_medicine_id,
        //                    PrescriptionId = pm.prescription_id,
        //                    MedicineName = pm.medicine?.medicine_name,
        //                    MedicineId = pm.medicine_id,
        //                    Quantity = pm.quantity,

        //                }).ToList()
        //            }).ToList()

        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error finding medical incident for student {Name}", studentCode);
        //        throw;
        //    }
        //}
        public async Task<List<MedicalIncidentDTOResponse>> GetByStudentCodeOrByNameAsync(string studentCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(studentCode))
                {
                    _logger.LogWarning("studentCode is null or empty.");
                    return new List<MedicalIncidentDTOResponse>();
                }

                var student = await _unitOfWork.Students.GetStudentByStudentCode(studentCode);

                if (student == null)
                {
                    _logger.LogWarning("Student not found with studentCode: {studentCode}", studentCode);
                    return new List<MedicalIncidentDTOResponse>();
                }

                var medicalIncidents = await _unitOfWork.MedicalIncidents.GetByStudentIdAsync(student.student_id);

                if (medicalIncidents == null)
                {
                    _logger.LogInformation("No medical incidents found for studentCode: {studentCode}", studentCode);
                    return new List<MedicalIncidentDTOResponse>();
                }

                var responseList = _mapper.Map<List<MedicalIncidentDTOResponse>>(medicalIncidents);

                return responseList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving medical incidents for studentCode: {studentCode}", studentCode);
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
