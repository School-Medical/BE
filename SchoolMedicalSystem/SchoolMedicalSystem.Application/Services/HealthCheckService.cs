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
    public class HealthCheckService : IHealthCheckService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger<HealthCheckService> _logger;

        public HealthCheckService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<HealthCheckService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<HealthCheckResponse>> GetAllHealthChecksAsync()
        {
            var healthChecks = await _unitOfWork.HealthChecks.GetAllAsync();
            var result = _mapper.Map<List<HealthCheckResponse>>(healthChecks);
            return result;
        }

        public async Task<HealthCheckResponse?> GetHealthCheckByIdAsync(int id)
        {
            var result = _mapper.Map<HealthCheckResponse>(await _unitOfWork.HealthChecks.GetByIdAsync(id));
            return result;
        }

        public async Task<HealthCheckResponse> CreateHealthCheckAsync(HealthCheckRequest healthCheckRequest)
        {
            var entity = _mapper.Map<HealthCheck>(healthCheckRequest);
            entity.status = 1;

            // Create health check
            await _unitOfWork.HealthChecks.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var healthCheckId = entity.health_check_id;

            // Get all students for create confirmation
            var studentList = await _unitOfWork.Students.GetAll();
            var confirmations = new List<HealthCheckConfirmation>();

            foreach (var student in studentList)
            {
                 var parent = await _unitOfWork.StudentParents.GetStudentParentByStudentIdAsync(student.student_id);
                if (parent == null)
                {
                    _logger.LogWarning($"No parent found for student ID {student.student_id}. Skipping confirmation creation.");
                    continue;
                }
                var confirmation = new HealthCheckConfirmation
                {
                    student_id = student.student_id,
                    health_check_id = healthCheckId,
                    parent_id = parent.user_id,
                    submit_at = null, // Not submitted yet
                    description = null,
                    status = 0 // Not confirmed (0 = chưa xác nhận)
                };

                confirmations.Add(confirmation);
            }

            if (confirmations.Any())
            {
                foreach (var confirmation in confirmations)
                {
                    await _unitOfWork.HealthCheckConfirmations.AddAsync(confirmation);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            _logger.LogInformation($"Created HealthCheck with ID: {healthCheckId} and {confirmations.Count} confirmations");

            // Return response
            var result = _mapper.Map<HealthCheckResponse>(entity);
            return result;

        }

        public async Task<HealthCheckResponse> UpdateHealthCheckAsync(int id, HealthCheckRequest healthCheck)
        {
            try
            {
                // Get existing health check
                var existingHealthCheck = await _unitOfWork.HealthChecks.GetByIdAsync(id);
                if (existingHealthCheck == null)
                {
                    _logger.LogWarning($"HealthCheck with ID: {id} not found");
                    throw new KeyNotFoundException($"HealthCheck with ID: {id} not found");
                }

                // Map updated values from request to existing entity
                _mapper.Map(healthCheck, existingHealthCheck);

                // Keep the original ID and maintain status (or update if needed)
                existingHealthCheck.health_check_id = id;
                // existingHealthCheck.status = 1; // Uncomment if you want to ensure status remains active

                // Update the health check
                await _unitOfWork.HealthChecks.UpdateAsync(existingHealthCheck);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Updated HealthCheck with ID: {id}");

                // Return updated response
                var result = _mapper.Map<HealthCheckResponse>(existingHealthCheck);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating HealthCheck with ID: {id}");
                throw;
            }
        }

        public async Task<PaginatedResponse<HealthCheckResponse>> GetPagedHealthChecksAsync(int pageNumber, int pageSize)
        {
            var query = await _unitOfWork.HealthChecks.GetAllAsync(); // or IQueryable if possible
            var totalRecords = query.Count();

            var pagedData = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = _mapper.Map<List<HealthCheckResponse>>(pagedData);

            return new PaginatedResponse<HealthCheckResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalRecords,
                Items = result
            };
        }


        public Task<PaginatedResponse<HealthCheckResponse>> GetHealthChecksPaginatedAsync(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HealthCheckResponse>> GetActiveHealthChecksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HealthCheckResponse>> GetHealthChecksByStatusAsync(int status)
        {
            throw new NotImplementedException();
        }
    }
}
