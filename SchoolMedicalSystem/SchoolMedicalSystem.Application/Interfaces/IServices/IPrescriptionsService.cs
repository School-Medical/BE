using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IPrescriptionsService
    {
        Task<PrescriptionDTORespone?> GetByIdAsync(int id);
        Task<PrescriptionDTORespone> AddAsync(PrescriptionDTORequest entity);
        Task<bool> UpdateAsync(PrescriptionDTORequest entity);
        Task<bool> DeleteAsync(int id);
    }
}
