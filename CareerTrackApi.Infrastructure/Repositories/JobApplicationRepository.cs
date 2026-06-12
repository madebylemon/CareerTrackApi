using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerTrackApi.Application.Interfaces;
using CareerTrackApi.Domain.Entities;
using CareerTrackApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerTrackApi.Infrastructure.Repositories
{
    // Triển khai Repository cho JobApplication bằng EF Core
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly AppDbContext _context;

        public JobApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy toàn bộ đơn ứng tuyển của User, sắp xếp theo ngày nộp đơn mới nhất
        public async Task<IEnumerable<JobApplication>> GetAllByUserIdAsync(int userId)
        {
            return await _context.JobApplications
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.DateApplied)
                .ToListAsync();
        }

        // Lấy chi tiết đơn ứng tuyển của User theo Id
        public async Task<JobApplication?> GetByIdAndUserIdAsync(int id, int userId)
        {
            return await _context.JobApplications
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        }

        // Thêm đơn ứng tuyển mới
        public async Task AddAsync(JobApplication application)
        {
            await _context.JobApplications.AddAsync(application);
        }

        // Cập nhật đơn ứng tuyển
        public void Update(JobApplication application)
        {
            _context.JobApplications.Update(application);
        }

        // Xóa đơn ứng tuyển
        public void Delete(JobApplication application)
        {
            _context.JobApplications.Remove(application);
        }

        // Lưu thay đổi vào Database
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
