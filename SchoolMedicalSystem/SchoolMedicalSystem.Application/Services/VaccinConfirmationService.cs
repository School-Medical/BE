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
    public class VaccinConfirmationService : IVaccinConfirmationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VaccinConfirmationService> _logger;
        public VaccinConfirmationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<VaccinConfirmationService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<VaccinConfirmationDTOResponse>> GetAllVaccinConfirmationsAsync()
        {
            return _mapper.Map<IEnumerable<VaccinConfirmationDTOResponse>>(await _unitOfWork.VaccinConfirmations.GetAllAsync());
        }

        public async Task<VaccinConfirmationDTOResponse?> GetVaccinConfirmationByIdAsync(int id)
        {
            return _mapper.Map<VaccinConfirmationDTOResponse>(await _unitOfWork.VaccinConfirmations.GetByIdAsync(id));
        }

        public async Task<VaccinConfirmationDTOResponse> CreateVaccinConfirmationAsync(int parentId, VaccinConfirmationDTORequest vaccinConfirmation)
        {
            //Chỉ cho phép phụ huynh có ID hợp lệ tạo Vaccin Confirmation
            var parentValid = await _unitOfWork.StudentParents.CheckParentValid(parentId, vaccinConfirmation.StudentId!.Value);
            if (!parentValid)
            {
                _logger.LogError("Parent with ID {ParentId} is not valid for Student ID {StudentId}", parentId, vaccinConfirmation.StudentId);
                throw new ArgumentException($"Parent with ID {parentId} is not valid for Student ID {vaccinConfirmation.StudentId}");
            }
            var createdVaccinConfirmation = await _unitOfWork.VaccinConfirmations.CreateAsync(_mapper.Map<VaccinConfirmation>(vaccinConfirmation));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<VaccinConfirmationDTOResponse>(createdVaccinConfirmation);
        }

        public async Task<VaccinConfirmationDTOResponse> UpdateVaccinConfirmationAsync(int id, VaccinConfirmationDTORequest vaccinConfirmation)
        {
            var existingConfirmation = await _unitOfWork.VaccinConfirmations.GetByIdAsync(id);
            if (existingConfirmation == null)
            {
                _logger.LogWarning("Vaccin Confirmation with ID {Id} not found for update", id);
                throw new KeyNotFoundException($"Vaccin Confirmation with ID {id} not found.");
            }
            _mapper.Map(vaccinConfirmation, existingConfirmation);
            var updated = await _unitOfWork.VaccinConfirmations.UpdateAsync(existingConfirmation);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<VaccinConfirmationDTOResponse>(updated);
        }

        public async Task<bool> DeleteVaccinConfirmationAsync(int id)
        {
            var existingConfirmation = await _unitOfWork.VaccinConfirmations.GetByIdAsync(id);
            if (existingConfirmation == null)
            {
                _logger.LogWarning("Vaccin Confirmation with ID {Id} not found for deletion", id);
                return false;
            }
            var deleted = await _unitOfWork.VaccinConfirmations.DeleteAsync(id);
            if (deleted)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return deleted;
        }

        public async Task<PaginatedResponse<VaccinConfirmationDTOResponse>> GetAllVaccinConfirmationsWithPagingAsync(int pageSize, int pageNumber)
        {
            var totalItems = await _unitOfWork.VaccinConfirmations.CountAsync();
            if (pageSize <= 0 || pageNumber <= 0 || pageSize > totalItems)
            {
                _logger.LogWarning("Invalid paging parameters: pageSize={PageSize}, pageNumber={PageNumber}", pageSize, pageNumber);
                return new PaginatedResponse<VaccinConfirmationDTOResponse>();
            }
            var items = _mapper.Map<IEnumerable<VaccinConfirmationDTOResponse>>(await _unitOfWork.VaccinConfirmations.GetAllWithPagingAsync(pageSize, pageNumber));

            return new PaginatedResponse<VaccinConfirmationDTOResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = items,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }

        public async Task<VaccinConfirmationDTOResponse?> GetVaccinConfirmationByStudentIdAsync(int studentId)
        {
            return _mapper.Map<VaccinConfirmationDTOResponse>(await _unitOfWork.VaccinConfirmations.GetVaccinConfirmationByStudentIdAsync(studentId));
        }

        /// <summary>
        /// Hàm này sẽ check phụ huynh có ghi xác nhận tiêm chủng cho học sinh hay không nếu không thì sẽ thông báo cho phụ huynh biết điền phiếu đi
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<VaccinConfirmationDTOResponse?> GetVaccinConfirmationByParentIdAsync(int parentId)
        {
            var entity = await _unitOfWork.VaccinConfirmations.GetVaccinConfirmationByParentIdAsync(parentId);
            if (entity == null)
            {
                _logger.LogError("Parent with ID {ParentId} has not filled out the Vaccin Confirmation form", parentId);
                throw new ArgumentException($"Parent with ID {parentId} has not filled out the Vaccin Confirmation form");
            }

            var confirmationDto = _mapper.Map<VaccinConfirmationDTOResponse>(entity);

            _logger.LogInformation("Parent with ID {ParentId} has filled out the Vaccin Confirmation form", parentId);
            return confirmationDto;

        }
    }
}
