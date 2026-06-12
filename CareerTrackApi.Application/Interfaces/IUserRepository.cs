using System.Threading.Tasks;
using CareerTrackApi.Domain.Entities;

namespace CareerTrackApi.Application.Interfaces
{
    // Giao diện Repository cho User
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<bool> SaveChangesAsync();
    }
}
