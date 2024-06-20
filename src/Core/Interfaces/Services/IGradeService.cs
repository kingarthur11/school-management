using Models.Requests;
using Models.Responses;
using Shared.Models.Responses;

namespace Core.Interfaces.Services
{
    public interface IGradeService
    {
        public Task<BaseResponse> CreateGrade(CreateGradeRequest request, string tenantId);
       
        public Task<ApiResponse<List<GradeResponse>>> GetAllAsync(string tenantId);
    }
}
