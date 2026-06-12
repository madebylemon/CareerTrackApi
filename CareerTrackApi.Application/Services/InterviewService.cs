using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerTrackApi.Application.DTOs.Interview;
using CareerTrackApi.Application.Interfaces;
using CareerTrackApi.Domain.Entities;

namespace CareerTrackApi.Application.Services
{
    // Dịch vụ quản lý vòng phỏng vấn
    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository _repository;
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public InterviewService(IInterviewRepository repository, IJobApplicationRepository jobApplicationRepository)
        {
            _repository = repository;
            _jobApplicationRepository = jobApplicationRepository;
        }

        // Lấy tất cả vòng phỏng vấn của đơn ứng tuyển (phải kiểm tra đúng User sở hữu đơn)
        public async Task<IEnumerable<InterviewResponseDto>?> GetAllByApplicationIdAsync(int applicationId, int userId)
        {
            var app = await _jobApplicationRepository.GetByIdAndUserIdAsync(applicationId, userId);
            if (app == null) return null; // Không tìm thấy đơn ứng tuyển hoặc không thuộc về User này

            var interviews = await _repository.GetAllByApplicationIdAndUserIdAsync(applicationId, userId);
            return interviews.Select(i => MapToDto(i)).ToList();
        }

        // Tạo mới vòng phỏng vấn (phải kiểm tra đúng User sở hữu đơn)
        public async Task<InterviewResponseDto?> CreateAsync(int applicationId, CreateInterviewRequestDto request, int userId)
        {
            var app = await _jobApplicationRepository.GetByIdAndUserIdAsync(applicationId, userId);
            if (app == null) return null; // Không tìm thấy đơn ứng tuyển hoặc không thuộc về User này

            var interview = new Interview
            {
                JobApplicationId = applicationId,
                InterviewType = request.InterviewType,
                ScheduledAt = request.ScheduledAt,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(interview);
            await _repository.SaveChangesAsync();

            return MapToDto(interview);
        }

        // Cập nhật thông tin vòng phỏng vấn
        public async Task<InterviewResponseDto?> UpdateAsync(int id, UpdateInterviewRequestDto request, int userId)
        {
            var interview = await _repository.GetByIdAndUserIdAsync(id, userId);
            if (interview == null) return null;

            interview.InterviewType = request.InterviewType;
            interview.ScheduledAt = request.ScheduledAt;
            interview.Notes = request.Notes;

            _repository.Update(interview);
            await _repository.SaveChangesAsync();

            return MapToDto(interview);
        }

        // Xóa vòng phỏng vấn
        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var interview = await _repository.GetByIdAndUserIdAsync(id, userId);
            if (interview == null) return false;

            _repository.Delete(interview);
            return await _repository.SaveChangesAsync();
        }

        // Helper map Entity sang DTO
        private static InterviewResponseDto MapToDto(Interview interview)
        {
            return new InterviewResponseDto
            {
                Id = interview.Id,
                JobApplicationId = interview.JobApplicationId,
                InterviewType = interview.InterviewType,
                ScheduledAt = interview.ScheduledAt,
                Notes = interview.Notes,
                CreatedAt = interview.CreatedAt
            };
        }
    }
}
