using System;
using CareerTrackApi.Domain.Enums;

namespace CareerTrackApi.Application.DTOs.Interview
{
    // DTO thông tin vòng phỏng vấn trả về
    public class InterviewResponseDto
    {
        public int Id { get; set; }
        public int JobApplicationId { get; set; }
        public InterviewType InterviewType { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
