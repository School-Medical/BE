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
    public class VaccinCampaignService : IVaccinCampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VaccinCampaignService> _logger;

        public VaccinCampaignService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<VaccinCampaignService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<VaccinCampaignDTOResponse>> GetAllVaccinCampaignsAsync()
        {
            return _mapper.Map<IEnumerable<VaccinCampaignDTOResponse>>(await _unitOfWork.VaccinCampaigns.GetAllAsync());
        }

        public async Task<VaccinCampaignDTOResponse?> GetVaccinCampaignByIdAsync(int id)
        {
            return _mapper.Map<VaccinCampaignDTOResponse>(await _unitOfWork.VaccinCampaigns.GetByIdAsync(id));
        }

        //public async Task<VaccinCampaignDTOResponse> CreateVaccinCampaignAsync(VaccinCampaignDTORequest vaccinCampaign)
        //{
        //    var createdVaccinCampaign = await _unitOfWork.VaccinCampaigns.CreateAsync(_mapper.Map<VaccinCampaign>(vaccinCampaign));
        //    await _unitOfWork.SaveChangesAsync();
        //    return _mapper.Map<VaccinCampaignDTOResponse>(createdVaccinCampaign);
        //}
        public async Task<VaccinCampaignDTOResponse> CreateVaccinCampaignAsync(VaccinCampaignDTORequest vaccinCampaign)
        {
            // Map request to entity
            var vaccinCampaignEntity = _mapper.Map<VaccinCampaign>(vaccinCampaign);
            vaccinCampaignEntity.status = 1;

            // Add to DB
            await _unitOfWork.VaccinCampaigns.CreateAsync(vaccinCampaignEntity);
            await _unitOfWork.SaveChangesAsync();

            var campaignId = vaccinCampaignEntity.vaccin_campaign_id;

            // Get all students
            var studentList = await _unitOfWork.Students.GetAll();
            var confirmations = new List<VaccinConfirmation>();

            foreach (var student in studentList)
            {
                var parent = await _unitOfWork.StudentParents.GetStudentParentByStudentIdAsync(student.student_id);
                if (parent == null)
                {
                    _logger.LogWarning($"No parent found for student ID {student.student_id}. Skipping vaccin confirmation creation.");
                    continue;
                }

                var confirmation = new VaccinConfirmation
                {
                    student_id = student.student_id,
                    campaign_id = campaignId,
                    parent_id = parent.user_id,
                    submit_at = null,
                    message = null,
                    status = 0
                };


                confirmations.Add(confirmation);
            }

            if (confirmations.Any())
            {
                foreach (var confirmation in confirmations)
                {
                    await _unitOfWork.VaccinConfirmations.CreateAsync(confirmation);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            _logger.LogInformation($"Created VaccinCampaign with ID: {campaignId} and {confirmations.Count} confirmations");

            // Return mapped response
            return _mapper.Map<VaccinCampaignDTOResponse>(vaccinCampaignEntity);
        }


        public async Task<VaccinCampaignDTOResponse> UpdateVaccinCampaignAsync(int id, VaccinCampaignDTORequest vaccinCampaign)
        {
            var existingCampaign = await _unitOfWork.VaccinCampaigns.GetByIdAsync(id);
            if (existingCampaign == null)
            {
                _logger.LogWarning("Vaccin Campaign with ID {Id} not found for update", id);
                throw new KeyNotFoundException($"Vaccin Campaign with ID {id} not found.");
            }
            _mapper.Map(vaccinCampaign, existingCampaign);
            var updated = await _unitOfWork.VaccinCampaigns.UpdateAsync(existingCampaign);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<VaccinCampaignDTOResponse>(updated);
        }

        public async Task<bool> DeleteVaccinCampaignAsync(int id)
        {
            var existingCampaign = await _unitOfWork.VaccinCampaigns.GetByIdAsync(id);
            if (existingCampaign == null)
            {
                _logger.LogWarning("Vaccin Campaign with ID {Id} not found for deletion", id);
                return false;
            }
            var deleted = await _unitOfWork.VaccinCampaigns.DeleteAsync(id);
            if (deleted)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return deleted;
        }

        public async Task<PaginatedResponse<VaccinCampaignDTOResponse>> GetVaccinCampaignsPaginatedAsync(int pageSize, int pageNumber)
        {
            var totalItems = await _unitOfWork.VaccinCampaigns.CountAsync();
            var items = await _unitOfWork.VaccinCampaigns.GetAllWithPagingAsync(pageSize, pageNumber);
            var mappedItems = _mapper.Map<IEnumerable<VaccinCampaignDTOResponse>>(items);
            return new PaginatedResponse<VaccinCampaignDTOResponse>
            {
                TotalItems = totalItems,
                Items = mappedItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            };
        }

        public async Task<VaccinCampaignDTOResponse?> GetCurrentCampaignAsync()
        {
            var currentCampaign = await _unitOfWork.VaccinCampaigns.GetCurrentCampaignAsync();
            return _mapper.Map<VaccinCampaignDTOResponse>(currentCampaign);
        }
    }
}
