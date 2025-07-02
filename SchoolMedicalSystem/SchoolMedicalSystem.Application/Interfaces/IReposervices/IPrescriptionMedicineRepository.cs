using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IPrescriptionMedicineRepository
    {
        Task<PrescriptionMedicine?> GetByIdAsync(int id);
        Task<PrescriptionMedicine> AddAsync(PrescriptionMedicine entity);
        Task<bool> UpdateAsync(PrescriptionMedicine entity);
        Task<bool> DeleteAsync(int id);
        Task<List<PrescriptionMedicine>> GetByPrescriptionId(int prescriptionId);
        Task<bool> DeleteByPrescriptionIdAsync(int prescriptionId);
    }
}
