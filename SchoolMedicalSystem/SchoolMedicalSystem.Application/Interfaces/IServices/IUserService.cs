using SchoolMedicalSystem.Application.DTO.Request;
using SchoolMedicalSystem.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<PaginatedResponse<UserDTOResponse>> GetAllAsync(int pageSize, int pageNumber);
        Task<UserDTOResponse?> GetByIdAsync(int userId);

        Task<UserDTOResponse> UpdateAsync(int userId, UserDTORequest userDto);
        Task<List<UserDTOResponse>> GetAllNursesAsync();
    }
}
