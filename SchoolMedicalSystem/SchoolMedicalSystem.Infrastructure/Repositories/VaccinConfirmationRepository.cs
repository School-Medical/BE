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
            return await _dbContext.VaccinConfirmations
                .Include(v => v.campaign)
                .Include(v => v.student)
                .Include(v => v.parent).ToListAsync();
        }

        public async Task<IEnumerable<VaccinConfirmation>> GetAllWithPagingByCampaignIdAsync(int pageSize, int pageNumber, int campaignId)
        {
            return await _dbContext.VaccinConfirmations
                .Include(v => v.student)
                .Include(v => v.parent)
                .Include(v => v.campaign)
                .Where(v => v.campaign_id == campaignId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<VaccinConfirmation?> GetByIdAsync(int id)
        {
            return await _dbContext.VaccinConfirmations.FindAsync(id);
        }

        public async Task<VaccinConfirmation> UpdateAsync(VaccinConfirmation confirmation)
        {
            _dbContext.VaccinConfirmations.Update(confirmation);
            return confirmation;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.VaccinConfirmations.CountAsync();
        }

        public async Task<int> CountByCampaignIdAsync(int campaignId)
        {
            return await _dbContext.VaccinConfirmations
                .Where(v => v.campaign_id == campaignId)
                .CountAsync();
        }

        public async Task<VaccinConfirmation?> GetVaccinConfirmationByStudentIdAsync(int studentId)
        {
            return await _dbContext.VaccinConfirmations
                .Include(v => v.student)
                .Include(v => v.parent)
                .Include(v => v.campaign)
                .Where(v => v.student_id == studentId)
                .FirstOrDefaultAsync();
        }

        //lấy ra VaccinConfirmation của các học sinh có phụ huynh là parentId
        public async Task<VaccinConfirmation?> GetVaccinConfirmationByParentIdAsync(int parentId)
        {
            return await _dbContext.VaccinConfirmations
                .Include(v => v.student)
                .Include(v => v.parent)
                .Include(v => v.campaign)
                .Where(v => v.parent_id == parentId)
                .FirstOrDefaultAsync();
        }

        public async Task<VaccinConfirmation?> GetVaccinConfirmationByStudentAndCampaignIdAsync(int studentId, int campaignId)
        {
            return await _dbContext.VaccinConfirmations
                .Include(v => v.student)
                .Include(v => v.parent)
                .Include(v => v.campaign)
                .Where(v => v.student_id == studentId && v.campaign_id == campaignId)
                .FirstOrDefaultAsync();
        }

    }
}
