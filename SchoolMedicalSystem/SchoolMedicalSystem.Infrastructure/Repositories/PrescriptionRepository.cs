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
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly SchoolMedicalDbContext _dbContext;
        public PrescriptionRepository(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Prescription> AddAsync(Prescription entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prescription = await _dbContext.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return false; // Prescription not found
            }
            _dbContext.Remove(prescription);
            return true;
        }

        public async Task<List<Prescription>> GetAllAsync()
        {
            return await _dbContext.Prescriptions.ToListAsync();
        }

        public async Task<List<Prescription>> GetPagedAsync(int pageSize, int pageNumber)
        {
            return await _dbContext.Prescriptions
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Prescription?> GetByIdAsync(int id)
        {
            return await _dbContext.Prescriptions.FirstOrDefaultAsync(x => x.prescription_id == id);
        }

        public async Task<bool> UpdateAsync(Prescription entity)
        {
            _dbContext.Update(entity);
            return true; // Assuming update always succeeds, consider adding error handling
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Prescriptions.CountAsync();
        }

        public async Task<Prescription?> GetByMedicalIncidentIdAsync(int incidentId)
        {
            return await _dbContext.Prescriptions
                .FirstOrDefaultAsync(p => p.medical_incident_id == incidentId);
        }

        public async Task<int> DeleteByMedicalIncidentIdAsync(int medicalIncidentId)
        {
            var prescription = await _dbContext.Prescriptions
                .FirstOrDefaultAsync(p => p.medical_incident_id == medicalIncidentId);

            if (prescription == null)
            {
                return -1; // Prescription not found
            }
            _dbContext.Remove(prescription);
            return prescription.prescription_id;
        }
    }
}
