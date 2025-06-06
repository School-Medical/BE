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
            if(medicalIncident == null)
            {
                return false;
                throw new Exception("Medical Incident not found");
            }
            _dbContext.Remove(medicalIncident);
            return true;
        }

        public async Task<List<MedicalIncident>> GetAllAsync()
        {
            return await _dbContext.MedicalIncidents.Include(m => m.nurse).Include(m => m.student).ToListAsync();
        }

        public async Task<MedicalIncident?> GetByIdAsync(int id)
        {
            return await _dbContext.MedicalIncidents.FirstOrDefaultAsync(x => x.medical_incident_id == id);
        }

        public  async Task<bool> UpdateAsync(MedicalIncident entity)
        {
            _dbContext.Update(entity);
            return true;
        }
    }
}
