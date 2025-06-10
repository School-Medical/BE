using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.ExceptionHandler;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Domain.Entities;

namespace SchoolMedicalSystem.Application.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<Medication> _logger;

        public MedicationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Medication> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MedicationResponse> AddAsync(MedicationRequest request)
        {
            var entity = _mapper.Map<Medication>(request);
            await _unitOfWork.Medication.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<MedicationResponse>(entity);
        }

        public Task<MedicationResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MedicationResponse>> GetAllAsync()
        {
            var entities = await _unitOfWork.Medication.GetAllAsync();
            return _mapper.Map<List<MedicationResponse>>(entities);
        }

        public async Task<MedicationResponse> GetByNameAsync(string name)
        {
            var entity = await _unitOfWork.Medication.GetByNameAsync(name);
            if (entity == null)
                throw new NotFoundException($"Không tìm thấy thuốc với id = {name}");

            return _mapper.Map<MedicationResponse>(entity);
        }

        public async Task<MedicationResponse> UpdateAsync(MedicationRequest request)
        {
            var existing = await _unitOfWork.Medication.GetByNameAsync(request.MedicineName);
            if (existing == null)
                throw new NotFoundException($"Không tìm thấy thuốc với tên = {request.MedicineName}");
            _mapper.Map(request, existing);
            await _unitOfWork.Medication.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<MedicationResponse>(existing);
        }
        
    }
}
