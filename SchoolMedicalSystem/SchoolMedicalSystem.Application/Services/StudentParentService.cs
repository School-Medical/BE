using AutoMapper;
using SchoolMedicalSystem.Application.DTO.Request;
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
    public class StudentParentService : IStudentParentService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public StudentParentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<StudentParent> AddAsync(StudentParentDTORequest studentParentDto)
        {
            if (studentParentDto == null)
            {
                throw new ArgumentNullException(nameof(studentParentDto), "StudentParent cannot be null");
            }
            var studentParent = _mapper.Map<StudentParent>(studentParentDto);
            studentParent = await _unitOfWork.StudentParent.AddAsync(studentParent);
            await _unitOfWork.SaveChangesAsync();
            return studentParent;
        }

        public Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            var result = _unitOfWork.StudentParent.DeleteAsync(id);
            _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
