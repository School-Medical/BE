using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IStudentParentService
    {
        Task<StudentParent> AddAsync(StudentParentDTORequest studentParentDto);
    }
}
