using System.ComponentModel.DataAnnotations;

namespace CareerTrackApi.Application.DTOs.Auth
{
    // DTO yêu cầu đăng nhập
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; } = string.Empty;
    }
}
