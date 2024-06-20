using Models.Requests;
using Shared.Models.Requests;
using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IQrCodeService
    {
        public Task<ApiResponse<List<StudentInSchoolResponse>>> GetTodaysQrCodeAsync(string email, string tenantId);
        public Task<ApiResponse<GenerateQrCodeResponse>> CreateQrCodeAsync(GenerateQrCodeRequest request, string tenantId);
        public Task<BaseResponse> AuthorizeQrCode(AuthorizeQrCodeRequest request);
        public Task<ApiResponse<List<StudentWithQrCodeResponse>>> GetParentStudentsAsync(string email, string tenantId);
        public Task<ApiResponse<List<GenerateQrCodeResponse>>> GenerateQrCodesForTripAsync(Guid tripId, string busDriverEmail, string tenantId);
        public Task<ApiResponse<GenerateQrCodeResponse>> GenerateQrCodeForTripAsync(Guid tripId, string busDriverEmail, string tenantId);

        public Task<ApiResponse<ScanQrCodeResponse>> ScanQrCodeForStudentAsync(string qrCodeData, string user, string tenantId);
        public Task<ApiResponse<ScanQrCodeBusDriverResponse>> ScanQrCodeForBusDriverAsync(string qrCodeData, string user, string tenantId);
    }
}
