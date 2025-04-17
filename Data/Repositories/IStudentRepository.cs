using StudentManagement.Models;

namespace StudentManagement.Data.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
    }
}