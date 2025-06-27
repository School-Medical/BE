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

        public async Task<VaccinCampaignDTOResponse> CreateVaccinCampaignAsync(VaccinCampaignDTORequest vaccinCampaign)
        {
            var createdVaccinCampaign = await _unitOfWork.VaccinCampaigns.CreateAsync(_mapper.Map<VaccinCampaign>(vaccinCampaign));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<VaccinCampaignDTOResponse>(createdVaccinCampaign);
        }

        public async Task<bool> UpdateVaccinCampaignAsync(int id, VaccinCampaignDTORequest vaccinCampaign)
        {
            var existingCampaign = await _unitOfWork.VaccinCampaigns.GetByIdAsync(id);
            if (existingCampaign == null)
            {
                _logger.LogWarning("Vaccin Campaign with ID {Id} not found for update", id);
                return false;
            }
            var updated = await _unitOfWork.VaccinCampaigns.UpdateAsync(_mapper.Map<VaccinCampaign>(vaccinCampaign));
            if (updated)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return updated;
        }

        public async Task<bool> DeleteVaccinCampaignAsync(int id)
        {
            var deleted = await _unitOfWork.VaccinCampaigns.DeleteAsync(id);
            if (deleted)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return deleted;
        }
    }
}
