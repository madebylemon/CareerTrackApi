using System;
using System.Collections.Generic;
using CareerTrackApi.Domain.Enums;

namespace CareerTrackApi.Domain.Entities
{
    // Đơn ứng tuyển công việc
    public class JobApplication
    {
        public int Id { get; set; }

        // Khóa ngoại liên kết tới User
        public int UserId { get; set; }
        public User? User { get; set; }

        // Tên công ty
        public string CompanyName { get; set; } = string.Empty;

        // Vị trí ứng tuyển
        public string RoleTitle { get; set; } = string.Empty;

        // Trạng thái đơn ứng tuyển
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Saved;

        // Ngày nộp đơn
        public DateTime DateApplied { get; set; }

        // Ghi chú thêm
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Danh sách vòng phỏng vấn của đơn này
        public List<Interview> Interviews { get; set; } = new();
    }
}
