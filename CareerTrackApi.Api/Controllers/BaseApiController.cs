using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace CareerTrackApi.Api.Controllers
{
    // Controller cơ sở chứa thông tin User đăng nhập
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        // Lấy UserId từ Claims trong JWT Token
        protected int CurrentUserId
        {
            get
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return int.TryParse(userIdClaim, out var userId) ? userId : 0;
            }
        }
    }
}
