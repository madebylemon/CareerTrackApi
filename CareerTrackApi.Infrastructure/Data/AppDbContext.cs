using CareerTrackApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerTrackApi.Infrastructure.Data
{
    // DbContext chính của ứng dụng
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<JobApplication> JobApplications => Set<JobApplication>();
        public DbSet<Interview> Interviews => Set<Interview>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Email phải là duy nhất
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Một User có nhiều JobApplication
            modelBuilder.Entity<JobApplication>()
                .HasOne(a => a.User)
                .WithMany(u => u.JobApplications)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Một JobApplication có nhiều Interview
            modelBuilder.Entity<Interview>()
                .HasOne(i => i.JobApplication)
                .WithMany(a => a.Interviews)
                .HasForeignKey(i => i.JobApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lưu enum dạng string cho dễ đọc trong DB
            modelBuilder.Entity<JobApplication>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Interview>()
                .Property(i => i.InterviewType)
                .HasConversion<string>();
        }
    }
}
