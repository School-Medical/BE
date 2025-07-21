using Microsoft.EntityFrameworkCore;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
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
    public class MedicalIncidentRepository : IMedicalIncidentRepository
    {
        private readonly SchoolMedicalDbContext _dbContext;

        public MedicalIncidentRepository(SchoolMedicalDbContext dbContext) => _dbContext = dbContext;

        public async Task<MedicalIncident> AddAsync(MedicalIncident entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medicalIncident = await _dbContext.MedicalIncidents.FindAsync(id);
            if (medicalIncident == null)
            {
                return false;
            }
            _dbContext.Remove(medicalIncident);
            return true;
        }

        #region AsQueryable

        /// <summary>
        /// Sử dụng IQueryable để tối ưu hiệu năng khi truy vấn dữ liệu lớn từ database.
        /// Phương thức trả về IQueryable, cho phép tiếp tục lọc, phân trang, sắp xếp trực tiếp trên database,
        /// thay vì lấy toàn bộ dữ liệu lên bộ nhớ rồi mới xử lý.
        /// </summary>
        /// <returns>IQueryable để xây dựng truy vấn LINQ trên database</returns>
        private IQueryable<MedicalIncident> GetPagedAsync()
        {
            return _dbContext.MedicalIncidents.AsQueryable();
        }

        #endregion

        public async Task<List<MedicalIncident>> GetPagedAsync(int pageSize, int pageNumber)
        {
            return await _dbContext.MedicalIncidents.Include(m => m.student).Include(m => m.nurse)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<MedicalIncident?> GetByIdAsync(int id)
        {
            return await _dbContext.MedicalIncidents
                                    .Include(m => m.student)
                                    .Include(m => m.nurse)
                                    .Include(m => m.Prescriptions)
                                    .Include(m => m.Prescriptions)
                                        .ThenInclude(p => p.PrescriptionMedicines)
                                        .ThenInclude(pm => pm.medicine)
                                    .FirstOrDefaultAsync(m => m.medical_incident_id == id);
        }

        public async Task<MedicalIncident?> GetByStudentIdAsync(int studentId)
        {
            return await _dbContext.MedicalIncidents               
                .Include(m => m.Prescriptions)
                    .ThenInclude(m => m.PrescriptionMedicines)
                    .ThenInclude(pm => pm.medicine)
                .Include(m => m.nurse)
                .FirstOrDefaultAsync(m => m.student_id == studentId);
        }

        public async Task<MedicalIncident> UpdateAsync(MedicalIncident entity)
        {
            _dbContext.Update(entity);
            return entity;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.MedicalIncidents.CountAsync();
        }
    }
}
