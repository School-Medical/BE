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
    public class VaccinDocumentService : IVaccinDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VaccinDocumentService> _logger;
        public VaccinDocumentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<VaccinDocumentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<VaccinDocumentDTOResponse>> GetAllVaccinDocumentsAsync()
        {
            return _mapper.Map<IEnumerable<VaccinDocumentDTOResponse>>(await _unitOfWork.VaccinDocuments.GetAllAsync());
        }

        public async Task<VaccinDocumentDTOResponse?> GetVaccinDocumentByIdAsync(int id)
        {
            var document = await _unitOfWork.VaccinDocuments.GetByIdAsync(id);
            if (document == null)
            {
                _logger.LogWarning("Vaccin Document with ID {Id} not found", id);
                throw new KeyNotFoundException($"Vaccin Document with ID {id} not found.");
            }
            return _mapper.Map<VaccinDocumentDTOResponse>(document);
        }

        /// <summary>
        /// Việc tạo mới Vaccin Document yêu cầu phải có Vaccin Confirmation hợp lệ cho Student ID.
        /// </summary>
        /// <param name="vaccinDocument"></param>
        /// <returns>VaccinDocumentDTOResponse</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<VaccinDocumentDTOResponse> CreateVaccinDocumentAsync(VaccinDocumentDTORequest vaccinDocument)
        {            
            if(vaccinDocument == null)
            {
                _logger.LogError("Vaccin Document creation failed: request is null");
                throw new ArgumentNullException(nameof(vaccinDocument), "Vaccin Document request cannot be null.");
            }

            var vaccinConfirmation = await _unitOfWork.VaccinConfirmations.GetVaccinConfirmationByStudentIdAsync(vaccinDocument.StudentId!.Value);
            if (vaccinConfirmation == null)
            {
                _logger.LogError("Vaccin Document creation failed: Student ID {StudentId} does not have a valid Vaccin Confirmation", vaccinDocument.StudentId);
                throw new ArgumentException($"Student ID {vaccinDocument.StudentId} does not have a valid Vaccin Confirmation before.");
            }

            var createdVaccinDocument = await _unitOfWork.VaccinDocuments.CreateAsync(_mapper.Map<VaccinDocument>(vaccinDocument));
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<VaccinDocumentDTOResponse>(createdVaccinDocument);
        }

        public async Task<bool> UpdateVaccinDocumentAsync(int id, VaccinDocumentDTORequest vaccinDocument)
        {
            var existingDocument = await _unitOfWork.VaccinDocuments.GetByIdAsync(id);
            if (existingDocument == null)
            {
                _logger.LogWarning("Vaccin Document with ID {Id} not found for update", id);
                return false;
            }
            var updated = await _unitOfWork.VaccinDocuments.UpdateAsync(_mapper.Map<VaccinDocument>(vaccinDocument));
            if (updated)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return updated;
        }

        public async Task<bool> DeleteVaccinDocumentAsync(int id)
        {
            var existingDocument = await _unitOfWork.VaccinDocuments.GetByIdAsync(id);
            if (existingDocument == null)
            {
                _logger.LogWarning("Vaccin Document with ID {Id} not found for deletion", id);
                return false;
            }
            var deleted = await _unitOfWork.VaccinDocuments.DeleteAsync(id);
            if (deleted)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return deleted;
        }

        public async Task<PaginatedResponse<VaccinDocumentDTOResponse>> GetAllVaccinDocumentsWithPagingAsync(int pageSize, int pageNumber)
        {
            var totalItems = await _unitOfWork.VaccinDocuments.CountAsync();
            if (pageSize <= 0 || pageNumber <= 0)
            {
                _logger.LogWarning("Invalid paging parameters: pageSize={PageSize}, pageNumber={PageNumber}", pageSize, pageNumber);
                throw new ArgumentException("Page size and number must be greater than zero.");
            }
            if (pageSize > totalItems)
            {
                _logger.LogWarning("Page size {PageSize} exceeds total items {TotalItems}", pageSize, totalItems);
                throw new ArgumentException("Page size exceeds total items.");
            }
            var items = _mapper.Map<IEnumerable<VaccinDocumentDTOResponse>>(await _unitOfWork.VaccinDocuments.GetAllWithPagingAsync(pageSize, pageNumber));

            return new PaginatedResponse<VaccinDocumentDTOResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items
            };
        }

        public async Task<VaccinDocument> GetVaccinDocumentByStudentIdAsync(int studentId)
        {
            var document = await _unitOfWork.VaccinDocuments.GetVaccinDocumentByStudentIdAsync(studentId);
            if (document == null)
            {
                _logger.LogWarning("Vaccin Document for Student ID {StudentId} not found", studentId);
                throw new KeyNotFoundException($"Vaccin Document for Student ID {studentId} not found.");
            }
            return document;
        }
    }
}
