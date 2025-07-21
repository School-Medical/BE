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
    public class HealthCheckRepository : GenericRepository<HealthCheck>, IHealthCheckRepository
    {
        public HealthCheckRepository(SchoolMedicalDbContext context) : base(context)
        {
        }

        public async Task<HealthCheck> CreateAsync(HealthCheck healthCheck)
        {
            await _dbSet.AddAsync(healthCheck);
            return healthCheck;
        }

        public override async Task<HealthCheck> GetByIdAsync(int id)
        {
            var result = await _dbSet
                .Include(hc => hc.HealthCheckConfirmations)
                .Include(hc => hc.HealthCheckDocuments)
                .FirstOrDefaultAsync(hc => hc.health_check_id == id);
            return result!;
        }

        public override async Task<IEnumerable<HealthCheck>> GetAllAsync()
        {
            return await _dbSet
                .Include(hc => hc.HealthCheckConfirmations)
                .Include(hc => hc.HealthCheckDocuments)
                .ToListAsync();
        }

        public override async Task<HealthCheck> UpdateAsync(HealthCheck healthCheck)
        {
            _dbSet.Update(healthCheck);
            await _context.SaveChangesAsync();
            return healthCheck;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var healthCheck = await _dbSet.FindAsync(id);
            if (healthCheck == null)
            {
                return false;
            }

            _dbSet.Remove(healthCheck);
            
            return true;
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<IEnumerable<HealthCheck>> GetAllWithPagingAsync(int pageSize, int pageNumber)
        {
            return await _dbSet
                .Include(hc => hc.HealthCheckConfirmations)
                .Include(hc => hc.HealthCheckDocuments)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<HealthCheck?> GetCurrentHealthCheckAsync()
        {
            var currentDate = DateTime.Now;
            return await _dbSet
                .Include(hc => hc.HealthCheckConfirmations)
                .Include(hc => hc.HealthCheckDocuments)
                .FirstOrDefaultAsync(hc => 
                    hc.start_at <= currentDate && 
                    hc.end_at >= currentDate &&
                    hc.status == 1); // Assuming 1 is active status
        }

        public async Task<IEnumerable<HealthCheck>> GetActiveHealthChecksAsync()
        {
            return await _dbSet
                .Include(hc => hc.HealthCheckConfirmations)
                .Include(hc => hc.HealthCheckDocuments)
                .Where(hc => hc.status == 1) // Assuming 1 is active status
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthCheck>> GetHealthChecksByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(hc => hc.HealthCheckConfirmations)
                .Include(hc => hc.HealthCheckDocuments)
                .Where(hc => hc.start_at >= startDate && hc.end_at <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthCheck>> GetHealthChecksByLocationAsync(string location)
        {
            return await _dbSet
                .Include(hc => hc.HealthCheckConfirmations)
                .Include(hc => hc.HealthCheckDocuments)
                .Where(hc => hc.location != null && hc.location.Contains(location))
                .ToListAsync();
        }

        public async Task<bool> IsHealthCheckActiveAsync(int healthCheckId)
        {
            var healthCheck = await _dbSet.FindAsync(healthCheckId);
            if (healthCheck == null) return false;

            var currentDate = DateTime.Now;
            return healthCheck.start_at <= currentDate && 
                   healthCheck.end_at >= currentDate && 
                   healthCheck.status == 1; // Assuming 1 is active status
        }

        public async Task<IEnumerable<HealthCheck>> GetUpcomingHealthChecksAsync()
        {
            var currentDate = DateTime.Now;
            return await _dbSet
                .Include(hc => hc.HealthCheckConfirmations)
                .Include(hc => hc.HealthCheckDocuments)
                .Where(hc => hc.start_at > currentDate && hc.status == 1)
                .OrderBy(hc => hc.start_at)
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthCheck>> GetHealthChecksWithConfirmationsAsync()
        {
            return await _dbSet
                .Include(hc => hc.HealthCheckConfirmations)
                .Where(hc => hc.HealthCheckConfirmations.Any())
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthCheck>> GetHealthChecksWithDocumentsAsync()
        {
            return await _dbSet
                .Include(hc => hc.HealthCheckDocuments)
                .Where(hc => hc.HealthCheckDocuments.Any())
                .ToListAsync();
        }

        public Task<IEnumerable<HealthCheck>> GetHealthChecksByStatusAsync(int status)
        {
            throw new NotImplementedException();
        }
    }
}
