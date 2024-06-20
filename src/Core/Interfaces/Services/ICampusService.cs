using Models.Requests;
using Models.Responses;
using Shared.Models.Responses;

namespace Core.Interfaces.Services
{
    public interface ICampusService
    {
        public Task<BaseResponse> CreateCampus(CreateCampusRequest request, string creator, string tenantId);
        public Task<ApiResponse<List<CampusResponse>>> GetAllAsync(string tenantId);
        public Task<ApiResponse<List<CampusResponse>>> GetCampusWithGradesAsync(string tenantId);
    }
}
