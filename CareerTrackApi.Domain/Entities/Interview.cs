using System;
using CareerTrackApi.Domain.Enums;

namespace CareerTrackApi.Domain.Entities
{
    // Vòng phỏng vấn thuộc một đơn ứng tuyển
    public class Interview
    {
        public int Id { get; set; }

        // Khóa ngoại liên kết tới JobApplication
        public int JobApplicationId { get; set; }
        public JobApplication? JobApplication { get; set; }

        // Loại phỏng vấn
        public InterviewType InterviewType { get; set; }

        // Thời gian phỏng vấn
        public DateTime ScheduledAt { get; set; }

        // Ghi chú chuẩn bị / nhận xét sau phỏng vấn
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
