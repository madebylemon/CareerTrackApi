using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerTrackApi.Application.Interfaces;
using CareerTrackApi.Domain.Entities;
using CareerTrackApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerTrackApi.Infrastructure.Repositories
{
    // Triển khai Repository cho Interview bằng EF Core
    public class InterviewRepository : IInterviewRepository
    {
        private readonly AppDbContext _context;

        public InterviewRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả cuộc phỏng vấn của đơn ứng tuyển (phải thuộc User tương ứng)
        public async Task<IEnumerable<Interview>> GetAllByApplicationIdAndUserIdAsync(int applicationId, int userId)
        {
            return await _context.Interviews
                .Include(i => i.JobApplication)
                .Where(i => i.JobApplicationId == applicationId && i.JobApplication!.UserId == userId)
                .OrderBy(i => i.ScheduledAt)
                .ToListAsync();
        }

        // Lấy chi tiết cuộc phỏng vấn theo Id (phải thuộc User tương ứng)
        public async Task<Interview?> GetByIdAndUserIdAsync(int id, int userId)
        {
            return await _context.Interviews
                .Include(i => i.JobApplication)
                .FirstOrDefaultAsync(i => i.Id == id && i.JobApplication!.UserId == userId);
        }

        // Thêm cuộc phỏng vấn mới
        public async Task AddAsync(Interview interview)
        {
            await _context.Interviews.AddAsync(interview);
        }

        // Cập nhật thông tin cuộc phỏng vấn
        public void Update(Interview interview)
        {
            _context.Interviews.Update(interview);
        }

        // Xóa cuộc phỏng vấn
        public void Delete(Interview interview)
        {
            _context.Interviews.Remove(interview);
        }

        // Lưu thay đổi vào Database
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
