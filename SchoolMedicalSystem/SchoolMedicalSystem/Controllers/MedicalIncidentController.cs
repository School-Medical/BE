using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Models;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalIncidentController : ControllerBase
    {
        private readonly IMedicalIncidentService _medicalIncidentService;
        public MedicalIncidentController(IMedicalIncidentService medicalIncidentService)
        {
            _medicalIncidentService = medicalIncidentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatMedicalIncident(MedicalIncidentDTORequest request)
        {
            try
            {
                var result = await _medicalIncidentService.AddAsync(request);
                return Ok(new ApiResponse<MedicalIncidentDTORequest>("Created is success!",result,200));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
            
            
        }
    }
}
