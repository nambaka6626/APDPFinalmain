using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync() => await _context.Users.ToListAsync();
        public async Task<User> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);

            // Nếu user là Student, thêm vào DbSet<Student> Students
            if (user is Student student)
            {
                await _context.Students.AddAsync(student);
            }

            await _context.SaveChangesAsync();
        }
    }
}