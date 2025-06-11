using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IMedicalSuppliesRepository
    {
        Task<List<Medicine>> GetAllasync();
        Task<Medicine?> GetByIdAsync(int id);
        Task<Medicine> AddAsync(Medicine entity);
        Task<bool> UpdateAsync(Medicine entity);
        Task<bool> DeleteAsync(int id);
        Task<List<Medicine>> GetMedicinesByCategoryAsync(string category);
    }
}
