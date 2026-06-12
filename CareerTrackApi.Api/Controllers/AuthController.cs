using System.Threading.Tasks;
using CareerTrackApi.Application.DTOs.Auth;
using CareerTrackApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareerTrackApi.Api.Controllers
{
    // Controller xử lý các chức năng xác thực
    [Route("api/auth")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Đăng ký tài khoản mới
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(request);
            if (result == null)
            {
                return BadRequest(new { Message = "Email đã được sử dụng." });
            }

            return Ok(result);
        }

        // Đăng nhập và lấy JWT token
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(request);
            if (result == null)
            {
                return Unauthorized(new { Message = "Email hoặc mật khẩu không đúng." });
            }

            return Ok(result);
        }
    }
}
