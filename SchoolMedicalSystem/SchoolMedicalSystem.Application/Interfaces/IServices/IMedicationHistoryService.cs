using SchoolMedicalSystem.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IMedicationHistoryService
    {
        Task<List<MedicationHistoryDTOResponse>> GetMedicationHistory(int id);
    }
}
