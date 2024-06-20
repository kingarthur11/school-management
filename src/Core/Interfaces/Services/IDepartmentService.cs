using Models.Requests;
using Models.Responses;
using Shared.Models.Requests;
using Shared.Models.Responses;

namespace Core.Interfaces.Services
{
    public interface IDepartmentService
    {
        public Task<BaseResponse> CreateDepartment(CreateDepartmentRequest request, string creator, string tenantId);
        public Task<ApiResponse<List<DepartmentResponse>>> GetAllAsync(string tenantId);
        Task<BaseResponse> UpdateDepartmentAsync(UpdateDepartmentRequest request);

    }
}
