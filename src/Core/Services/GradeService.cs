using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Models.Requests;
using Models.Responses;
using Shared.Models.Responses;

namespace Core.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<BaseResponse> CreateGrade(CreateGradeRequest request, string tenantId)
        {
            var grade = new Grade()
            {
                Name = request.Name,
                Arms = request.Arms,
                CampusId = request.CampusId,
                Level = request.Level,
                TenantId = tenantId,
            };

            var result = await _gradeRepository.AddGrade(grade);
            return result;
        }

        public async Task<ApiResponse<List<GradeResponse>>> GetAllAsync(string tenantId)
        {
            var grades = await _gradeRepository.GetAllAsync();

            return new ApiResponse<List<GradeResponse>>()
            {
                Data = await grades.Where(p => p.TenantId == tenantId).Select(x => new GradeResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Arms = x.Arms,
                    CampusName = x.Campus!.Name,
                    Level = x.Level
                }).ToListAsync()
            };
        }
    }
}
