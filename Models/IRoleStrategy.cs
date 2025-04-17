using StudentManagement.Data.Repositories;

namespace StudentManagement.Models
{
    public interface IRoleStrategy
    {
        Task<List<Student>> ViewStudentsAsync(User user, IStudentRepository repo);
    }
}