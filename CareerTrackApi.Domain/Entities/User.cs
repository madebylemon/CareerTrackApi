using System;
using System.Collections.Generic;

namespace CareerTrackApi.Domain.Entities
{
    // Người dùng hệ thống
    public class User
    {
        public int Id { get; set; }

        // Email dùng để đăng nhập
        public string Email { get; set; } = string.Empty;

        // Mật khẩu đã hash
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Danh sách đơn ứng tuyển của người dùng
        public List<JobApplication> JobApplications { get; set; } = new();
    }
}
