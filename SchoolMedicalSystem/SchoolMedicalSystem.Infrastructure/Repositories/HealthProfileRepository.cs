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
    public class HealthProfileRepository : IHealthProfileRepository
    {
        private readonly SchoolMedicalDbContext _dbContext;

        public HealthProfileRepository(SchoolMedicalDbContext dbContext) => _dbContext = dbContext;

        public async Task<HealthProfile> AddAsync(HealthProfile entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var healthProfile = await _dbContext.HealthProfiles.FindAsync(id);
            if (healthProfile == null)
            {
                return false; 
                throw new Exception("Health Profile not found");
            }
            _dbContext.Remove(healthProfile);
            return true;
        }

        public async Task<List<HealthProfile>> GetAllAsync()
        {
            return await _dbContext.HealthProfiles.Include(h => h.student).ToListAsync();
        }

        public async Task<List<HealthProfile>> GetPagedAsync(int pageSize, int pageNumber)
        {
            return await _dbContext.HealthProfiles.Include(h => h.student)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<HealthProfile?> GetByIdAsync(int id)
        {
            return await _dbContext.HealthProfiles.FirstOrDefaultAsync(x => x.health_profile_id == id);
        }

        public async Task<bool> UpdateAsync(HealthProfile entity)
        {
            _dbContext.Update(entity);
            return true;
        }
    }
}
