using StudentManagement.Data.Repositories;

namespace StudentManagement.Models
{
    public class StudentStrategy : IRoleStrategy
    {
        public async Task<List<Student>> ViewStudentsAsync(User user, IStudentRepository repo) =>
            (await repo.GetAllAsync()).Where(s => s.Id == user.Id).ToList();
    }
}