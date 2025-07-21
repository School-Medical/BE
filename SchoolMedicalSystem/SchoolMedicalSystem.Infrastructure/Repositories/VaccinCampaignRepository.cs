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
            return await _dbContext.VaccinCampaigns.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<VaccinCampaign?> GetByIdAsync(int id)
        {
            return await _dbContext.VaccinCampaigns.FindAsync(id);
        }

        public async Task<VaccinCampaign> UpdateAsync(VaccinCampaign campaign)
        {
            _dbContext.VaccinCampaigns.Update(campaign);
            return campaign;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.VaccinCampaigns.CountAsync();
        }

        //Chỉ lấy chiến dịch tiêm chủng hiện tại với staus 1 (đang hoạt động)
        public async Task<VaccinCampaign?> GetCurrentCampaignAsync()
        {
            return await _dbContext.VaccinCampaigns
                .Where(c => c.register_start <= DateTime.Now && c.register_close >= DateTime.Now && c.status == 1)
                .FirstOrDefaultAsync();
        }
    }
}
