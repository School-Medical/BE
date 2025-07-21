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
                var confirmation = new HealthCheckConfirmation
                {
                    student_id = student.student_id,
                    health_check_id = healthCheckId,
                    parent_id = student.parent_id, // Assuming student has parent_id property
                    submit_at = null, // Not submitted yet
                    description = null,
                    status = 0 // Not confirmed (0 = chưa xác nhận)
                };

                confirmations.Add(confirmation);
            }





        }

    }
}
