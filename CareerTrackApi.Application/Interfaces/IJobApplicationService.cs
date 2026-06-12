using System.Collections.Generic;
using System.Threading.Tasks;
using CareerTrackApi.Application.DTOs.JobApplication;

namespace CareerTrackApi.Application.Interfaces
{
    // Giao diện dịch vụ JobApplication
    public interface IJobApplicationService
    {
        Task<IEnumerable<JobApplicationResponseDto>> GetAllAsync(int userId);
        Task<JobApplicationResponseDto?> GetByIdAsync(int id, int userId);
        Task<JobApplicationResponseDto> CreateAsync(CreateJobApplicationRequestDto request, int userId);
        Task<JobApplicationResponseDto?> UpdateAsync(int id, UpdateJobApplicationRequestDto request, int userId);
        Task<JobApplicationResponseDto?> UpdateStatusAsync(int id, UpdateJobApplicationStatusDto request, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
