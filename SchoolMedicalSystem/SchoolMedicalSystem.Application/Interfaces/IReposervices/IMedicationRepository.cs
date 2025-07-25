﻿using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IMedicationRepository : IGenericRepository<Medication>
    {
        Task<Medication?> GetByNameAsync(string name);

        Task<List<Medication>> GetByGivenDoseId(int id);
    }
}
