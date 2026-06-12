using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerTrackApi.Application.DTOs.JobApplication;
using CareerTrackApi.Application.Interfaces;
using CareerTrackApi.Domain.Entities;

namespace CareerTrackApi.Application.Services
{
    // Dịch vụ quản lý đơn ứng tuyển
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repository;

        public JobApplicationService(IJobApplicationRepository repository)
        {
            _repository = repository;
        }

        // Lấy tất cả đơn ứng tuyển của User
        public async Task<IEnumerable<JobApplicationResponseDto>> GetAllAsync(int userId)
        {
            var apps = await _repository.GetAllByUserIdAsync(userId);
            return apps.Select(a => MapToDto(a)).ToList();
        }

        // Lấy đơn ứng tuyển theo Id (phải đúng UserId)
        public async Task<JobApplicationResponseDto?> GetByIdAsync(int id, int userId)
        {
            var app = await _repository.GetByIdAndUserIdAsync(id, userId);
            if (app == null) return null;
            return MapToDto(app);
        }

        // Tạo đơn ứng tuyển mới cho User
        public async Task<JobApplicationResponseDto> CreateAsync(CreateJobApplicationRequestDto request, int userId)
        {
            var app = new JobApplication
            {
                UserId = userId,
                CompanyName = request.CompanyName,
                RoleTitle = request.RoleTitle,
                Status = request.Status,
                DateApplied = request.DateApplied,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(app);
            await _repository.SaveChangesAsync();

            return MapToDto(app);
        }

        // Cập nhật toàn bộ đơn ứng tuyển
        public async Task<JobApplicationResponseDto?> UpdateAsync(int id, UpdateJobApplicationRequestDto request, int userId)
        {
            var app = await _repository.GetByIdAndUserIdAsync(id, userId);
            if (app == null) return null;

            app.CompanyName = request.CompanyName;
            app.RoleTitle = request.RoleTitle;
            app.Status = request.Status;
            app.DateApplied = request.DateApplied;
            app.Notes = request.Notes;
            app.UpdatedAt = DateTime.UtcNow;

            _repository.Update(app);
            await _repository.SaveChangesAsync();

            return MapToDto(app);
        }

        // Cập nhật trạng thái đơn ứng tuyển (PATCH)
        public async Task<JobApplicationResponseDto?> UpdateStatusAsync(int id, UpdateJobApplicationStatusDto request, int userId)
        {
            var app = await _repository.GetByIdAndUserIdAsync(id, userId);
            if (app == null) return null;

            app.Status = request.Status;
            app.UpdatedAt = DateTime.UtcNow;

            _repository.Update(app);
            await _repository.SaveChangesAsync();

            return MapToDto(app);
        }

        // Xóa đơn ứng tuyển
        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var app = await _repository.GetByIdAndUserIdAsync(id, userId);
            if (app == null) return false;

            _repository.Delete(app);
            return await _repository.SaveChangesAsync();
        }

        // Helper map Entity sang DTO
        private static JobApplicationResponseDto MapToDto(JobApplication app)
        {
            return new JobApplicationResponseDto
            {
                Id = app.Id,
                UserId = app.UserId,
                CompanyName = app.CompanyName,
                RoleTitle = app.RoleTitle,
                Status = app.Status,
                DateApplied = app.DateApplied,
                Notes = app.Notes,
                CreatedAt = app.CreatedAt,
                UpdatedAt = app.UpdatedAt
            };
        }
    }
}
