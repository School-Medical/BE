using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Services
{
    public class HealthCheckConfirmationService : IHealthCheckConfirmationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<HealthCheckConfirmationService> _logger;

        public HealthCheckConfirmationService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<HealthCheckConfirmationService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<HealthCheckConfirmationResponse> CreateHealthCheckConfirmationAsync(int parentId, HealthCheckConfirmationRequest healthCheckConfirmation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteHealthCheckConfirmationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HealthCheckConfirmationResponse>> GetAllHealthCheckConfirmationsAsync()
        {
            var confirmations = await _unitOfWork.HealthCheckConfirmations.GetAllAsync();
            var result = _mapper.Map<List<HealthCheckConfirmationResponse>>(confirmations);
            return result;
        }

        public async Task<HealthCheckConfirmationResponse?> GetHealthCheckConfirmationByIdAsync(int id)
        {
            var confirmation = await _unitOfWork.HealthCheckConfirmations.GetByIdAsync(id);
            if (confirmation == null)
                return null;

            var result = _mapper.Map<HealthCheckConfirmationResponse>(confirmation);
            return result;
        }

        public async Task<HealthCheckConfirmationResponse?> GetHealthCheckConfirmationByStudentIdAsync(int studentId, int healthCheckId)
        {
            var confirmation = await _unitOfWork.HealthCheckConfirmations.GetByHealthCheckAndStudentAsync(healthCheckId, studentId);
            if (confirmation == null)
                return null;

            var result = _mapper.Map<HealthCheckConfirmationResponse>(confirmation);
            return result;
        }

        public async Task<IEnumerable<HealthCheckConfirmationResponse>> GetHealthCheckConfirmationsByHealthCheckIdAsync(int healthCheckId)
        {
            var confirmations = await _unitOfWork.HealthCheckConfirmations.GetByHealthCheckIdAsync(healthCheckId);
            var result = _mapper.Map<List<HealthCheckConfirmationResponse>>(confirmations);
            return result;
        }

        public async Task<IEnumerable<HealthCheckConfirmationResponse>> GetHealthCheckConfirmationsByParentIdAsync(int parentId)
        {
            var confirmations = await _unitOfWork.HealthCheckConfirmations.GetByParentIdAsync(parentId);
            var result = _mapper.Map<List<HealthCheckConfirmationResponse>>(confirmations);
            return result;
        }

        public async Task<IEnumerable<HealthCheckConfirmationResponse>> GetHealthCheckConfirmationsByStatusAsync(int status)
        {
            var confirmations = await _unitOfWork.HealthCheckConfirmations.GetByStatusAsync((ulong)status);
            var result = _mapper.Map<List<HealthCheckConfirmationResponse>>(confirmations);
            return result;
        }

        public async Task<HealthCheckConfirmationResponse> UpdateHealthCheckConfirmationAsync(int id, HealthCheckConfirmationRequest healthCheckConfirmation)
        {
            try
            {
                var existing = await _unitOfWork.HealthCheckConfirmations.GetByIdAsync(id);
                if (existing == null)
                {
                    throw new KeyNotFoundException($"HealthCheckConfirmation with ID: {id} not found");
                }

                _mapper.Map(healthCheckConfirmation, existing);
                existing.hc_confirmation_id = id;
                existing.submit_at = DateTime.UtcNow;

                await _unitOfWork.HealthCheckConfirmations.UpdateAsync(existing);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Updated HealthCheckConfirmation with ID: {id}");

                var result = _mapper.Map<HealthCheckConfirmationResponse>(existing);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating HealthCheckConfirmation with ID: {id}");
                throw;
            }
        }
    }
}
