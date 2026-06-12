using System.Threading.Tasks;
using CareerTrackApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerTrackApi.Api.Controllers
{
    // Controller xử lý thông tin người dùng hiện tại
    [Authorize]
    [Route("api/users")]
    public class UsersController : BaseApiController
    {
        private readonly IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }

        // Lấy thông tin cá nhân của User đang đăng nhập
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = CurrentUserId;
            if (userId <= 0)
            {
                return Unauthorized(new { Message = "Không tìm thấy thông tin xác thực" });
            }

            var profile = await _authService.GetUserProfileAsync(userId);
            if (profile == null)
            {
                return NotFound(new { Message = "Không tìm thấy người dùng" });
            }

            return Ok(profile);
        }
    }
}
