using System;
using System.Threading.Tasks;
using CareerTrackApi.Application.DTOs.Auth;
using CareerTrackApi.Application.Interfaces;
using CareerTrackApi.Application.Security;
using CareerTrackApi.Domain.Entities;

namespace CareerTrackApi.Application.Services
{
    // Dịch vụ thực hiện đăng ký, đăng nhập và lấy hồ sơ
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // Đăng ký người dùng mới
        public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return null; // Email đã được sử dụng
            }

            var user = new User
            {
                Email = request.Email,
                PasswordHash = PasswordHasher.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token
            };
        }

        // Đăng nhập người dùng
        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return null; // Email hoặc mật khẩu sai
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token
            };
        }

        // Lấy thông tin cá nhân
        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return null;

            return new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
