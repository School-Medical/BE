 using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IGivenDoseRepository : IGenericRepository<GivenDose>
    {
        Task<GivenDose?> GetGivenDoseByStudentId(int studentId);
        Task<List<GivenDose>> GetByParentId(int parentId);
    }
}
