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
        private readonly IMapper _mapper;
        private readonly ILogger<GivenDoseService> _logger;

        public GivenDoseService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GivenDoseService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GivenDoseResponse> AddAsync(GivenDoseRequest request)
        {
            var entity = _mapper.Map<GivenDose>(request);
            entity.create_at = DateTime.UtcNow;

            await _unitOfWork.GivenDoses.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GivenDoseResponse>(entity);
        }

        public async Task<GivenDoseResponse> UpdateAsync(int id, GivenDoseRequest request)
        {
            var entity = await _unitOfWork.GivenDoses.GetByIdAsync(id)
            ?? throw new Exception("GivenDose not found");
            _mapper.Map(request, entity);

            _unitOfWork.GivenDoses.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GivenDoseResponse>(entity);
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
            catch (Exception e)
            {
                _logger.LogError(e, "Error search given dose by student name");
                throw;
            }
        }

        public async Task<List<GivenDoseResponse>> GetAllAsync()
        {
            var list = await _unitOfWork.GivenDoses.GetAllAsync();
            return _mapper.Map<List<GivenDoseResponse>>(list);
        }

        public async Task<GivenDoseResponse?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GivenDoses.GetByIdAsync(id);
            return _mapper.Map<GivenDoseResponse>(entity);
        }

        public async Task<List<GivenDoseResponse>> GetByParentId(int id)
        {
            var list = await _unitOfWork.GivenDoses.GetByParentId(id);
            return _mapper.Map<List<GivenDoseResponse>>(list);
        }
    }
}
