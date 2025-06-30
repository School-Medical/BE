using AutoMapper;
using Microsoft.Extensions.Logging;
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
    public class MedicationHistoryService : IMedicationHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicationHistoryService> _logger;

        public MedicationHistoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<MedicationHistoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<MedicationHistoryDTOResponse>> GetMedicationHistory(int id)
        {

            try
            {
                var list = await _unitOfWork.MedicationHistory.GetMedicationHistory(id);
                return _mapper.Map<List<MedicationHistoryDTOResponse>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading medication history");
                throw;
            }
        }
    }
}
