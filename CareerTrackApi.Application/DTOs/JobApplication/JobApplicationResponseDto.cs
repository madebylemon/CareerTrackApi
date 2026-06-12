using System;
using CareerTrackApi.Domain.Enums;

namespace CareerTrackApi.Application.DTOs.JobApplication
{
    // DTO thông tin đơn ứng tuyển trả về
    public class JobApplicationResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string RoleTitle { get; set; } = string.Empty;
        public ApplicationStatus Status { get; set; }
        public DateTime DateApplied { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
