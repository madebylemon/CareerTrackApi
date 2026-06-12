using System.Collections.Generic;
using System.Threading.Tasks;
using CareerTrackApi.Application.DTOs.Interview;

namespace CareerTrackApi.Application.Interfaces
{
    // Giao diện dịch vụ Interview
    public interface IInterviewService
    {
        Task<IEnumerable<InterviewResponseDto>?> GetAllByApplicationIdAsync(int applicationId, int userId);
        Task<InterviewResponseDto?> CreateAsync(int applicationId, CreateInterviewRequestDto request, int userId);
        Task<InterviewResponseDto?> UpdateAsync(int id, UpdateInterviewRequestDto request, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
