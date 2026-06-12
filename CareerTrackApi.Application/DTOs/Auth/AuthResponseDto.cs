namespace CareerTrackApi.Application.DTOs.Auth
{
    // DTO phản hồi đăng nhập thành công chứa JWT token
    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
