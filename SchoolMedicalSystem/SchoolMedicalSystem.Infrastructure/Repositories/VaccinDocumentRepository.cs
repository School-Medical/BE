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
    public class VaccinDocumentRepository : IVaccinDocumentRepository
    {

        private readonly SchoolMedicalDbContext _dbContext;

        public VaccinDocumentRepository(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VaccinDocument> CreateAsync(VaccinDocument campaign)
        {
            await _dbContext.VaccinDocuments.AddAsync(campaign);
            return campaign;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await _dbContext.VaccinCampaigns.FindAsync(id);
            if (existingEntity == null) return false;
            _dbContext.Remove(existingEntity);
            return true;
        }

        public async Task<IEnumerable<VaccinDocument>> GetAllAsync()
        {
            return await _dbContext.VaccinDocuments.Include(v => v.student).Include(v => v.nurse).Include(v=> v.vaccin_confirm).ToListAsync();
        }

        public async Task<IEnumerable<VaccinDocument>> GetAllWithPagingAsync(int pageSize, int pageNumber)
        {
            return await _dbContext.VaccinDocuments.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<VaccinDocument?> GetByIdAsync(int id)
        {
            return await _dbContext.VaccinDocuments.FindAsync(id);
        }

        public async Task<VaccinDocument> UpdateAsync(VaccinDocument document)
        {
            _dbContext.VaccinDocuments.Update(document);
            return document;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.VaccinDocuments.CountAsync();
        }

        public async Task<VaccinDocument?> GetVaccinDocumentByStudentIdAsync(int studentId)
        {
            return await _dbContext.VaccinDocuments
                .Where(v => v.student_id == studentId)
                .FirstOrDefaultAsync();
        }
    }
}
