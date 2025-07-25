﻿using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IMedicalIncidentService
    {
        Task<PaginatedResponse<MedicalIncidentDTOResponse>> GetAllAsync(int pageSize, int pageNumber);
        Task<MedicalIncidentDTOResponse?> GetByIdAsync(int medicalIncidentID);
        Task<MedicalIncidentDTOResponse> AddAsync(MedicalIncidentDTORequest medicalIncidentDTO);
        Task<MedicalIncidentDTOResponse> UpdateAsync(int medicalIncidentId, MedicalIncidentDTORequest dto);
        Task<bool> DeleteAsync(int medicalIncidentID);
        Task<List<MedicalIncidentDTOResponse>> GetByStudentCodeOrByNameAsync(string studentCode);
    }
}
