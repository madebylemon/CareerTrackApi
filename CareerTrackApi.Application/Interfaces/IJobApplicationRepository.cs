using System.Collections.Generic;
using System.Threading.Tasks;
using CareerTrackApi.Domain.Entities;

namespace CareerTrackApi.Application.Interfaces
{
    // Giao diện Repository cho JobApplication
    public interface IJobApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetAllByUserIdAsync(int userId);
        Task<JobApplication?> GetByIdAndUserIdAsync(int id, int userId);
        Task AddAsync(JobApplication application);
        void Update(JobApplication application);
        void Delete(JobApplication application);
        Task<bool> SaveChangesAsync();
    }
}
