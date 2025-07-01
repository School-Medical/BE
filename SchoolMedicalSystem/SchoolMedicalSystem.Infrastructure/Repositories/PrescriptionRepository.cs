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
            try
            {
                await _dbContext.Prescriptions.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException)
            {
                return null!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prescription = await _dbContext.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return false; // Prescription not found
            }

            try
            {
                _dbContext.Prescriptions.Remove(prescription);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
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
            if (id <= 0)
            {
                return null;
            }

            try
            {
                return await _dbContext.Prescriptions
                    .FirstOrDefaultAsync(x => x.prescription_id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<bool> UpdateAsync(Prescription entity)
        {
            // Check if the prescription exists in the database
            var existingPrescription = await _dbContext.Prescriptions
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.prescription_id == entity.prescription_id);

            if (existingPrescription == null)
            {
                return false;
            }

            try
            {
                _dbContext.Prescriptions.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
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
