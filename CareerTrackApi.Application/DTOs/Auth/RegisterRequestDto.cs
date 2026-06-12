using System.ComponentModel.DataAnnotations;

namespace CareerTrackApi.Application.DTOs.Auth
{
    // DTO yêu cầu đăng ký
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(100, ErrorMessage = "Email tối đa 100 ký tự")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6 đến 50 ký tự")]
        public string Password { get; set; } = string.Empty;
    }
}
