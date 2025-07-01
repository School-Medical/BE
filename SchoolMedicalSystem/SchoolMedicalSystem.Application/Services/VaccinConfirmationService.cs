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
using System.Net.WebSockets;
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
            try
            {
                //Chỉ cho phép phụ huynh có ID hợp lệ tạo Vaccin Confirmation
                var parentValid = await _unitOfWork.StudentParents.CheckParentValid(vaccinConfirmation.StudentId!.Value, parentId);
                if (!parentValid)
                {
                    _logger.LogError("Parent with ID {ParentId} is not valid for Student ID {StudentId}", parentId, vaccinConfirmation.StudentId);
                    throw new ArgumentException($"Parent with ID {parentId} is not valid for Student ID {vaccinConfirmation.StudentId}");
                }

                //Kiểm tra chiến dịch tiêm chủng nào có đang hoạt động không
                var campaign = await _unitOfWork.VaccinCampaigns.GetCurrentCampaignAsync();
                if (campaign == null)
                {
                    _logger.LogError("No active Vaccin Campaign found for Student ID {StudentId}", vaccinConfirmation.StudentId);
                    throw new ArgumentException($"No active Vaccin Campaign found for Student ID {vaccinConfirmation.StudentId}");
                }

                //Kiểm tra xem học sinh đã có xác nhận tiêm chủng nào chưa
                var existingConfirmation = await _unitOfWork.VaccinConfirmations.GetVaccinConfirmationByStudentAndCampaignIdAsync(vaccinConfirmation.StudentId.Value, campaign.vaccin_campaign_id);
                if (existingConfirmation != null)
                {
                    _logger.LogError("Vaccin Confirmation already exists for Student ID {StudentId} in Campaign ID {CampaignId}", vaccinConfirmation.StudentId, campaign.vaccin_campaign_id);
                    throw new ArgumentException($"Vaccin Confirmation already exists for Student ID {vaccinConfirmation.StudentId} in Campaign ID {campaign.vaccin_campaign_id}");
                }

                var createdVaccinConfirmation = await _unitOfWork.VaccinConfirmations.CreateAsync(_mapper.Map<VaccinConfirmation>(vaccinConfirmation));
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<VaccinConfirmationDTOResponse>(createdVaccinConfirmation);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating Vaccin Confirmation for Parent ID {ParentId}", parentId);
                throw;
            }           
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

        public async Task<VaccinCampainPagingDTOResponse> GetAllVaccinConfirmationsWithPagingAsync(int campaignId, int pageSize, int pageNumber)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                _logger.LogWarning("Invalid paging parameters: pageSize={PageSize}, pageNumber={PageNumber}", pageSize, pageNumber);
                throw new ArgumentException("Invalid paging parameters.");
            }

            var campaign = await _unitOfWork.VaccinCampaigns.GetByIdAsync(campaignId);
            if (campaign == null)
            {
                _logger.LogWarning("Campaign with ID {Id} not found", campaignId);
                throw new ArgumentException($"Campaign with ID {campaignId} not found.");
            }

            //if (campaign.status == 0)
            //{
            //    _logger.LogWarning("Campaign {Id} is not active", campaignId);
            //    throw new ArgumentException($"Campaign {campaignId} is not active.");
            //}

            var totalItems = await _unitOfWork.VaccinConfirmations.CountByCampaignIdAsync(campaignId);

            var items = await _unitOfWork.VaccinConfirmations
                .GetAllWithPagingByCampaignIdAsync(pageSize, pageNumber, campaignId);

            var mapped = _mapper.Map<IEnumerable<VaccinConfirmationDTOResponse>>(items);

            return new VaccinCampainPagingDTOResponse
            {
                CampaignId = campaignId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                //TotalCount = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                Items = mapped
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
        public async Task<VaccinCampaignDTOResponse> GetVaccinConfirmationsByParentIdAsync(int parentId)
        {
            //Lấy ra chiến dịch tiêm chủng hiện tại đang có
            var campaign = await _unitOfWork.VaccinCampaigns.GetCurrentCampaignAsync();

            var students = await _unitOfWork.StudentParents.GetStudentParentsByParentIdAsync(parentId);

            var studentStatusList = new List<StudentVaccinStatusDTO>();

            foreach (var student in students)
            {
                var confirmation = await _unitOfWork.VaccinConfirmations
                    .GetVaccinConfirmationByStudentAndCampaignIdAsync(student.student_id!.Value, campaign.vaccin_campaign_id);

                var studentStatus = new StudentVaccinStatusDTO
                {
                    StudentId = student.student_id!.Value,
                    StudentCode = student.student!.student_code ?? "",
                    StudentName = $"{student.student!.first_name} {student.student!.last_name}",
                    SubmitAt = confirmation?.submit_at,
                    StatusConfirm = confirmation?.status == 1
                };

                studentStatusList.Add(studentStatus);
            }

            return new VaccinCampaignDTOResponse
            {
                VaccinCampaignId = campaign.vaccin_campaign_id,
                CampaignName = campaign.campaign_name,
                CampaignDescription = campaign.campaign_description,
                StartAt = campaign.start_at,
                EndAt = campaign.end_at,
                Location = campaign.location,
                VaccinName = campaign.vaccin_name,
                VaccinDescription = campaign.vaccin_description,
                RegisterStart = campaign.register_start,
                RegisterClose = campaign.register_close,
                Status = campaign.status,
                VaccinNotice = campaign.vaccin_notice,
                StudentVaccinStatuses = studentStatusList
            };
        }

        
    }
}
