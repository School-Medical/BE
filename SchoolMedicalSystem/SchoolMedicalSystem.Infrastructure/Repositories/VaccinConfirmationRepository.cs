using Microsoft.EntityFrameworkCore;
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
    public class VaccinConfirmationRepository : IVaccinConfirmationRepository
    {
        private readonly SchoolMedicalDbContext _dbContext;
        
        public VaccinConfirmationRepository(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VaccinConfirmation> CreateAsync(VaccinConfirmation campaign)
        {
            await _dbContext.VaccinConfirmations.AddAsync(campaign);
            return campaign;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await _dbContext.VaccinConfirmations.FindAsync(id);
            if (existingEntity == null) return false;
            _dbContext.Remove(existingEntity);
            return true;
        }

        public async Task<IEnumerable<VaccinConfirmation>> GetAllAsync()
        {
            return await _dbContext.VaccinConfirmations.Include(v => v.student).Include(v => v.parent).ToListAsync();
        }

        public async Task<IEnumerable<VaccinConfirmation>> GetAllWithPagingAsync(int pageSize, int pageNumber)
        {
            return await _dbContext.VaccinConfirmations.Include(v => v.student).Include(v => v.parent).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<VaccinConfirmation?> GetByIdAsync(int id)
        {
            return await _dbContext.VaccinConfirmations.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(VaccinConfirmation campaign)
        {
            _dbContext.VaccinConfirmations.Update(campaign);
            return true;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.VaccinConfirmations.CountAsync();
        }

        public async Task<VaccinConfirmation?> GetVaccinConfirmationByStudentIdAsync(int studentId)
        {
            return await _dbContext.VaccinConfirmations
                .Where(v => v.student_id == studentId)
                .FirstOrDefaultAsync();
        }
    }
}
