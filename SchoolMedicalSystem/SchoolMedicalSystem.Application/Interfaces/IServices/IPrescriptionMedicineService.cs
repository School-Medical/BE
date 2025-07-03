using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IPrescriptionMedicineService
    {
        Task<PrescriptionMedicineDTORespone> GetByIdAsync(int id);
        Task<PrescriptionMedicineDTORespone> AddAsync(PrescriptionMedicineDTORequest entity);
        Task<List<PrescriptionMedicineDTORespone>> AddListAsynce(List<PrescriptionMedicineDTORequest> list);

        Task<bool> UpdateAsync(PrescriptionMedicineDTORequest entity);
        Task<bool> DeleteAsync(int id);
        Task<List<PrescriptionMedicineDTORespone>> GetByPrescriptionId(int prescriptionId);
    }
}
