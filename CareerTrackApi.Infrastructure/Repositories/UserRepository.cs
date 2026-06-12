using System.Threading.Tasks;
using CareerTrackApi.Application.Interfaces;
using CareerTrackApi.Domain.Entities;
using CareerTrackApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerTrackApi.Infrastructure.Repositories
{
    // Triển khai Repository cho User bằng EF Core
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // Tìm người dùng theo Id
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Tìm người dùng theo Email (để đăng nhập / kiểm tra trùng)
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Thêm người dùng mới
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        // Lưu thay đổi vào Database
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
