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
    public class VaccinCampaignRepository : IVaccinCampaignRepository
    {

        private readonly SchoolMedicalDbContext _dbContext;

        public VaccinCampaignRepository(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VaccinCampaign> CreateAsync(VaccinCampaign campaign)
        {
            await _dbContext.VaccinCampaigns.AddAsync(campaign);
            return campaign;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await _dbContext.VaccinCampaigns.FindAsync(id);
            if (existingEntity == null) return false;
            _dbContext.Remove(existingEntity);
            return true;
        }

        public async Task<IEnumerable<VaccinCampaign>> GetAllAsync()
        {
            return await _dbContext.VaccinCampaigns.ToListAsync();
        }

        public async Task<IEnumerable<VaccinCampaign>> GetAllWithPagingAsync(int pageSize, int pageNumber)
        {
            return await _dbContext.VaccinCampaigns.Skip((pageNumber-1)* pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<VaccinCampaign?> GetByIdAsync(int id)
        {
            return await _dbContext.VaccinCampaigns.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(VaccinCampaign campaign)
        {
            _dbContext.VaccinCampaigns.Update(campaign);
            return true;
        }
        public async Task<int> CountAsync()
        {
            return await _dbContext.VaccinCampaigns.CountAsync();
        }
    }
}
