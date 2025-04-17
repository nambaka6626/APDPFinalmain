using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data.Repositories;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepo;

        public AccountController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string role)
        {
            var user = (await _userRepo.GetAllAsync())
                .FirstOrDefault(u => u.Username == username && u.Password == password && u.Role.ToString() == role);
            if (user == null) return View();

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Role", user.Role.ToString());

            return user.Role switch
            {
                Role.Admin => RedirectToAction("Index", "Admin"),
                Role.Faculty => RedirectToAction("Index", "Faculty"),
                Role.Student => RedirectToAction("Index", "Student"),
                _ => RedirectToAction("Login")
            };
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(Student student)
        {
            var existingUser = (await _userRepo.GetAllAsync()).FirstOrDefault(u => u.Username == student.Username);
            if (existingUser != null) return View();

            student.Role = Role.Student;
            await _userRepo.AddAsync(student);
            return RedirectToAction("Login");
        }

        public IActionResult CreateFaculty()
        {
            if (HttpContext.Session.GetString("Role") != Role.Admin.ToString())
                return Unauthorized();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFaculty(string username, string password)
        {
            if (HttpContext.Session.GetString("Role") != Role.Admin.ToString())
                return Unauthorized();

            var existingUser = (await _userRepo.GetAllAsync()).FirstOrDefault(u => u.Username == username);
            if (existingUser != null) return View();

            var faculty = new Student
            {
                Username = username,
                Password = password,
                Role = Role.Faculty,
                Name = username,
                Class = "N/A"
            };
            await _userRepo.AddAsync(faculty);
            return RedirectToAction("Index", "Admin");
        }
        // Thêm hành động Logout
        public IActionResult Logout()
        {
            // Xóa session của người dùng
            HttpContext.Session.Clear();
            // Chuyển hướng về trang Login
            return RedirectToAction("Login", "Account");
        }
    }
}