using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data.Repositories;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepo;

        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != Role.Admin.ToString())
                return Unauthorized();
            var courses = await _courseRepo.GetAllAsync();
            return View(courses);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != Role.Admin.ToString())
                return Unauthorized();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (HttpContext.Session.GetString("Role") != Role.Admin.ToString())
                return Unauthorized();
            await _courseRepo.AddAsync(course);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("Role") != Role.Admin.ToString())
                return Unauthorized();
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            if (HttpContext.Session.GetString("Role") != Role.Admin.ToString())
                return Unauthorized();
            await _courseRepo.UpdateAsync(course);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != Role.Admin.ToString())
                return Unauthorized();
            await _courseRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}