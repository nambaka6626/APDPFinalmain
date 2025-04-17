using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data.Repositories;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ICourseRepository _courseRepo;
        private IRoleStrategy _strategy;

        public StudentController(IStudentRepository studentRepo, ICourseRepository courseRepo)
        {
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
        }

        private void SetStrategy(Role role)
        {
            _strategy = role switch
            {
                Role.Student => new StudentStrategy(),
                _ => throw new ArgumentException("Invalid role for Student.")
            };
        }

        public async Task<IActionResult> Index()
        {
            var user = new Student
            {
                Id = int.Parse(HttpContext.Session.GetString("UserId")),
                Role = Enum.Parse<Role>(HttpContext.Session.GetString("Role"))
            };
            SetStrategy(user.Role);
            var students = await _strategy.ViewStudentsAsync(user, _studentRepo);
            return View(students);
        }

        // Thêm action để xem danh sách khóa học
        public async Task<IActionResult> ViewCourses()
        {
            if (HttpContext.Session.GetString("Role") != Role.Student.ToString())
                return Unauthorized();

            var courses = await _courseRepo.GetAllAsync();
            return View(courses);
        }
    }
}