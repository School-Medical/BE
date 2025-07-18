using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Domain.Entities;
using SchoolMedicalSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Infrastructure.Repositories
{
    public class StudentParentRepository : IStudentParentRepository
    {
        public readonly SchoolMedicalDbContext _dbContext;
        public StudentParentRepository(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<StudentParent> AddAsync(StudentParent studentParent)
        {
            if (studentParent == null)
            {
                throw new ArgumentNullException(nameof(studentParent), "StudentParent cannot be null");
            }
            _dbContext.StudentParents.Add(studentParent);
            return Task.FromResult(studentParent);
        }
    }
}
