using System;

namespace CareerTrackApi.Application.DTOs.Auth
{
    // DTO thông tin cá nhân của người dùng
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
