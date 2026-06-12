using CareerTrackApi.Domain.Entities;

namespace CareerTrackApi.Application.Interfaces
{
    // Giao diện tạo JWT Token
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
