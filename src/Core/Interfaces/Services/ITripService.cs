using Models.Responses;
using Shared.Models.Requests;
using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ITripService
    {
        public Task<ApiResponse<Guid?>> CreateTripAsync(CreateTripRequest request, string driver, string tenantId);
        public Task<ApiResponse<List<TripResponse>>> TripListAsync(string tenantId);
        public Task<BaseResponse> AddStudentToTripAsync(AddStudentToTripRequest request, string tenantId);
        public Task<ApiResponse<IEnumerable<StudentResponse>>> GetNotOnboardedStudentAsync(Guid tripId, string busDriverEmail, string tenantId);
        public Task<ApiResponse<IEnumerable<StudentResponse>>> GetOnboardedStudentAsync(Guid tripId, string busDriverEmail, string tenantId);
        public Task<BaseResponse> RemoveStudentFromTripAsync(RemoveStudentFromTripRequest request);
    }
}
