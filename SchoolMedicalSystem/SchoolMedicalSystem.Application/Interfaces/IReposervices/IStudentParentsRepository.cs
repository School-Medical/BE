using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IStudentParentsRepository
    {
        Task<StudentParent> CreateAsync(StudentParent studentParents);
        Task<StudentParent?> GetByIdAsync(int id);
        Task<IEnumerable<StudentParent>> GetAllAsync();
        Task<bool> UpdateAsync(StudentParent studentParents);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<StudentParent>> GetAllWithPagingAsync(int pageSize, int pageNumber);
        Task<int> CountAsync();
        Task<StudentParent?> GetStudentParentByStudentIdAsync(int studentId);
        Task<bool> CheckParentValid(int studentId, int parentId);
    }
}
