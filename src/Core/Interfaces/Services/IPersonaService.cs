﻿using Models.Requests;
using Models.Responses;
using Shared.Models.Requests;
using Shared.Models.Responses;

namespace Core.Interfaces.Services
{
    public interface IPersonaService
    {
        public Task<ApiResponse<ParentResponse>> CreateParentAsync(CreateParentRequest request, string host, string tenantId, string email);
        public Task<ApiResponse<List<ParentResponse>>> ParentListAsync(string tenantId);
        // public Task<BaseResponse> EditParentAsync(Guid parentId, EditParentRequest request, string editor);
        public Task<ApiResponse<ParentResponse>> GetParentAsync(Guid parentId, string tenantId);
        public Task<BaseResponse> DeleteParentAsync(Guid parentId, string deletor);
        public Task<ApiResponse<StudentResponse>> CreateStudentAsync(CreateStudentRequest request, string host, string tenantId, string email);
        public Task<ApiResponse<List<StudentResponse>>> StudentListAsync(string tenantId);
        public Task<ApiResponse<StudentResponse>> GetStudentAsync(Guid studentId, string tenantId);
        public Task<BaseResponse> DeleteStudnetAsync(Guid studentId, string deletor);
        public Task<ApiResponse<List<StudentResponse>>> ParentStudentsListAsync(Guid parentId, string tenantId);
        public Task<BaseResponse> CreateBusDriverAsync(CreateBusDriverRequest request, string host, string tenantId, string email);
        public Task<BaseResponse> CreateStaffAsync(CreateStaffRequest request, string host, string tenantId, string email);
        public Task<ApiResponse<List<StaffResponse>>> StaffListAsync(string tenantId);
        public Task<ApiResponse<List<BusDriverResponse>>> BusDriverListAsync(string tenantId);

        // public Task<ApiResponse<StudentResponse>> EditStudentAsync(Guid studentId, EditStudentRequest request, string editor);
        public Task<BaseResponse> DeletePersonaAccountAsync(string email);
        public Task<BaseResponse> UpdateParentInfoAsync(Guid parentId, UpdateParentInfoRequest request, string editor);
    }
}
