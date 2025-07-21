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
    public class HealthCheckDocumentService : IHealthCheckDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<HealthCheckDocumentService> _logger;

        public HealthCheckDocumentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<HealthCheckDocumentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<HealthCheckDocumentResponse>> GetAllHealthCheckDocumentsAsync()
        {
            var documents = await _unitOfWork.HealthCheckDocuments.GetAllAsync();
            return _mapper.Map<IEnumerable<HealthCheckDocumentResponse>>(documents);
        }

        public async Task<HealthCheckDocumentResponse> GetHealthCheckDocumentByIdAsync(int id)
        {
            var document = await _unitOfWork.HealthCheckDocuments.GetByIdAsync(id);
            if (document == null)
                throw new Exception("Document not found");

            return _mapper.Map<HealthCheckDocumentResponse>(document);
        }

        public async Task<HealthCheckDocumentResponse> CreateHealthCheckDocumentAsync(HealthCheckDocumentRequest request)
        {
            var entity = _mapper.Map<HealthCheckDocument>(request);
            entity.check_at = DateTime.UtcNow;

            await _unitOfWork.HealthCheckDocuments.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<HealthCheckDocumentResponse>(entity);
        }

        public async Task<HealthCheckDocumentResponse> UpdateHealthCheckDocumentAsync(int id, HealthCheckDocumentRequest request)
        {
            var document = await _unitOfWork.HealthCheckDocuments.GetByIdAsync(id);
            if (document == null)
                throw new Exception("Document not found");

            // Update fields
            _mapper.Map(request, document); // update all mapped fields
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<HealthCheckDocumentResponse>(document);
        }

        public async Task<bool> DeleteHealthCheckDocumentAsync(int id)
        {
            var document = await _unitOfWork.HealthCheckDocuments.GetByIdAsync(id);
            if (document == null)
                return false;

            _unitOfWork.HealthCheckDocuments.DeleteAsync(document.hc_document_id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<HealthCheckDocumentResponse>> GetDocumentsByHealthCheckIdAsync(int healthCheckId)
        {
            var docs = await _unitOfWork.HealthCheckDocuments.GetDocumentsByHealthCheckIdAsync(healthCheckId);
            return _mapper.Map<IEnumerable<HealthCheckDocumentResponse>>(docs);
        }

        public async Task<IEnumerable<HealthCheckDocumentResponse>> GetDocumentsByConfirmationIdAsync(int confirmationId)
        {
            var docs = await _unitOfWork.HealthCheckDocuments.GetDocumentsByConfirmationIdAsync(confirmationId);
            return _mapper.Map<IEnumerable<HealthCheckDocumentResponse>>(docs);
        }

        public async Task<HealthCheckConfirmationResponse> GetDocumentByStudentAndHealthCheckId(int studentId, int healthCheckId)
        {
            var doc = await _unitOfWork.HealthCheckDocuments.GetDocumentByStudentAndHealthCheckIdAsync(studentId, healthCheckId);
            if (doc == null)
                throw new Exception("Document not found");

            var confirmation = await _unitOfWork.HealthCheckConfirmations
                .GetByIdAsync(doc.health_check_confirm_id ?? 0);

            if (confirmation == null)
                throw new Exception("Confirmation not found");

            return _mapper.Map<HealthCheckConfirmationResponse>(confirmation);
        }
    }
}
