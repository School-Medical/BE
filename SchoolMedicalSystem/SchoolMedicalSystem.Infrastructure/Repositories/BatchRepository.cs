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
    public class BatchRepository : IBatchRepository
    {
        private readonly SchoolMedicalDbContext _context;
        public BatchRepository(SchoolMedicalDbContext context)
        {
            _context = context;
        }


        public async Task<Batch> AddBatchAsync(Batch entity)
        {
            await _context.Batches.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteBatchAsync(int id)
        {
            var entity = await _context.Batches.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _context.Batches.Remove(entity);
            return true;
        }
    }
}
