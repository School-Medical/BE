using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Domain.Entities;


namespace SchoolMedicalSystem.Application.Services
{
    public class GivenDoseService : IGivenDoseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedicationService _medicationService;
        private readonly IMapper _mapper;
        private readonly ILogger<GivenDoseService> _logger;

        public GivenDoseService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GivenDoseService> logger, IMedicationService medicationService)
        {
            _unitOfWork = unitOfWork;
            _medicationService = medicationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GivenDoseResponse> AddAsync(GivenDoseRequest request)
        {
            try
            {
                var entity = _mapper.Map<GivenDose>(request);
                entity.create_at = DateTime.UtcNow;

                await _unitOfWork.GivenDoses.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                var givenDoesId = entity.given_dose_id;

                if (request.MedicationRequestList != null && request.MedicationRequestList.Any())
                {
                    foreach (var item in request.MedicationRequestList)
                    {
                        item.GivenDoseId = givenDoesId;
                        await _unitOfWork.Medications.AddAsync(_mapper.Map<Medication>(item));
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                var result = await _unitOfWork.GivenDoses.GetByIdAsync(givenDoesId);

                return _mapper.Map<GivenDoseResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding GivenDose.");
                throw;
            }
        }

        public async Task<GivenDoseResponse> UpdateAsync(int id, GivenDoseRequest request)
        {
            try
            {
                var entity = await _unitOfWork.GivenDoses.GetByIdAsync(id)
                    ?? throw new Exception("GivenDose not found");

                _mapper.Map(request, entity);

                _unitOfWork.GivenDoses.UpdateAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<GivenDoseResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating GivenDose with id {id}.");
                throw;
            }
        }

        public async Task<List<GivenDoseResponse>> SearchByStudentNameAsync(string studentName)
        {
            try
            {
                var studentList = await _unitOfWork.Students.GetStudentByName(studentName);

                List<GivenDose> givenDoseList = new List<GivenDose>();

                foreach (Student student in studentList)
                {
                    var result = await _unitOfWork.GivenDoses.GetGivenDoseByStudentId(student.student_id);
                    if (result != null)
                        givenDoseList.Add(result);
                }

                return _mapper.Map<List<GivenDoseResponse>>(givenDoseList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while searching GivenDose by student name: {StudentName}", studentName);
                throw;
            }
        }

        public async Task<List<GivenDoseResponse>> GetAllAsync()
        {
            try
            {
                var list = await _unitOfWork.GivenDoses.GetAllAsync();
                return _mapper.Map<List<GivenDoseResponse>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all GivenDose records.");
                throw;
            }
        }

        public async Task<GivenDoseResponse?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.GivenDoses.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("GivenDose with id {Id} not found.", id);
                    return null;
                }

                return _mapper.Map<GivenDoseResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting GivenDose by id: {Id}", id);
                throw;
            }
        }

        public async Task<List<GivenDoseResponse>> GetByParentId(int id)
        {
            try
            {
                var list = await _unitOfWork.GivenDoses.GetByParentId(id);
                return _mapper.Map<List<GivenDoseResponse>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting GivenDose by parent id: {ParentId}", id);
                throw;
            }
        }
    }
}
