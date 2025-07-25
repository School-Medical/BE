﻿using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface IMedicationHistoryRepository
    {
        Task<List<GivenDose>> GetMedicationHistoryAsync(int studentId);
    }
}
