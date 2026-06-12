using System.Collections.Generic;
using System.Threading.Tasks;
using CareerTrackApi.Domain.Entities;

namespace CareerTrackApi.Application.Interfaces
{
    // Giao diện Repository cho Interview
    public interface IInterviewRepository
    {
        Task<IEnumerable<Interview>> GetAllByApplicationIdAndUserIdAsync(int applicationId, int userId);
        Task<Interview?> GetByIdAndUserIdAsync(int id, int userId);
        Task AddAsync(Interview interview);
        void Update(Interview interview);
        void Delete(Interview interview);
        Task<bool> SaveChangesAsync();
    }
}
