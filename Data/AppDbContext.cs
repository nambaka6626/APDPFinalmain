using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình TPT
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Student>()
                .ToTable("Students");

            // Dữ liệu khởi tạo
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123",
                    Role = Role.Admin,
                    Name = "Administrator",
                    Class = "N/A"
                }
            );
        }
    }
}