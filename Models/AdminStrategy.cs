using StudentManagement.Data.Repositories;

namespace StudentManagement.Models
{
    public class AdminStrategy : IRoleStrategy
    {
        public async Task<List<Student>> ViewStudentsAsync(User user, IStudentRepository repo) =>
            await repo.GetAllAsync();
    }
}