using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class TeacherController(EduHomeDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            var teacher = _context.Teachers
                .Include(t => t.TeacherImages)
                .Include(t => t.TeacherDetail)
                .Include(t => t.TeacherHobbies)
                    .ThenInclude(tt => tt.Hobbie)
                .Include(t => t.TeacherSkills)
                    .ThenInclude(tt => tt.Skill)
                .FirstOrDefault(t => t.Id == id);
            if (teacher == null) return BadRequest();

            TeacherVM teacherVM = new()
            {
                Teacher = new Teacher()
                {
                    Name = teacher.Name,
                    Profession = teacher.Profession,
                    TeacherImages = teacher.TeacherImages,
                    TeacherDetail = teacher.TeacherDetail,
                    TeacherHobbies = teacher.TeacherHobbies,
                },
                Skills = teacher.TeacherSkills.Select(s => s.Skill)
            };
            return View(teacherVM);
        }
    }
}
