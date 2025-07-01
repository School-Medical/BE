using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IBatchRepository
    {
        Task<Batch> AddBatchAsync(Batch entity);
        Task<bool> DeleteBatchAsync(int id);
    }
}
