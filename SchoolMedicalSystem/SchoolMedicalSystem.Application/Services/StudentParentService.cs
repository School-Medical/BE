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
            return await _unitOfWork.StudentParent.AddAsync(studentParent);
        }
    }
}
