using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data.Repositories;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IUserRepository _userRepo;
        private IRoleStrategy _strategy;

        public AdminController(IStudentRepository studentRepo, IUserRepository userRepo)
        {
            _studentRepo = studentRepo;
            _userRepo = userRepo;
        }

        private void SetStrategy(Role role)
        {
            _strategy = role switch
            {
                Role.Admin => new AdminStrategy(),
                _ => throw new ArgumentException("Invalid role for Admin.")
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

        // Thêm action để xem toàn bộ tài khoản (Users)
        public async Task<IActionResult> ViewAllUsers()
        {
            if (HttpContext.Session.GetString("Role") != Role.Admin.ToString())
                return Unauthorized();

            var users = await _userRepo.GetAllAsync();
            return View(users);
        }
    }
}