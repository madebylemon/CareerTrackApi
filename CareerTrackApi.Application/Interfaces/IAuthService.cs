using System.Threading.Tasks;
using CareerTrackApi.Application.DTOs.Auth;

namespace CareerTrackApi.Application.Interfaces
{
    // Giao diện dịch vụ xác thực
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
    }
}
