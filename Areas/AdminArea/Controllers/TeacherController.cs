using EduHome.Areas.AdminArea.ViewModel.Teacher;
using EduHome.DAL;
using FiorelloApp.Areas.AdminArea.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class TeacherController(EduHomeDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var teachers = _context.Teachers
                .Include(c => c.TeacherImages)
                .Include(c => c.TeacherDetail)
                .Include(c => c.TeacherHobbies)
                    .ThenInclude(c => c.Hobbie)
                .Include(c => c.TeacherSkills)
                    .ThenInclude(c => c.Skill)
                .ToList();
            return View(teachers);

        }
        public async Task<IActionResult> Detail(int id)
        {
            if (id == null) return NotFound();
            var teacher = await _context.Teachers
                .Include(c => c.TeacherImages)
                .Include(c => c.TeacherDetail)
                .Include(c => c.TeacherHobbies)
                    .ThenInclude(c => c.Hobbie)
                .Include(c => c.TeacherSkills)
                    .ThenInclude(c => c.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teacher == null) return NotFound();

            TeacherDetailVM teacherDetailVM = new TeacherDetailVM()
            {
                Name = teacher.Name,
                Profession = teacher.Profession,
                TeacherImages = teacher.TeacherImages,
                TeacherDetail = teacher.TeacherDetail,
                TeacherSkills = teacher.TeacherSkills.Select(s => s.Skill).ToList(),
                TeacherHobbies = teacher.TeacherHobbies.Select(s => s.Hobbie).ToList(),
            };

            return View(teacherDetailVM);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var teacher = await _context.Teachers
                .Include(c => c.TeacherImages)
                .Include(c => c.TeacherDetail)
                .Include(c => c.TeacherHobbies)
                    .ThenInclude(c => c.Hobbie)
                .Include(c => c.TeacherSkills)
                    .ThenInclude(c => c.Skill)
               .FirstOrDefaultAsync(i => i.Id == id);
            if (teacher == null) return NotFound();
            var image = teacher.TeacherImages.FirstOrDefault(i => i.TeacherId == id);
            if (image == null) return NotFound();
            Helper.DeleteImageFromFolder(image.ImageURL, "teacher");
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
