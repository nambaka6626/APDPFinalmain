using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync() => await _context.Students.ToListAsync();
        public async Task<Student> GetByIdAsync(int id) => await _context.Students.FindAsync(id);
    }
}