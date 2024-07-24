using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class CourseController(EduHomeDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int? id, string? name)
        {
            if (id == null) return BadRequest();

            var course = await _context.Course
            .Include(c => c.CourseImages)
            .Include(c => c.CourseFeatures)
            .Include(c => c.CourseCategories)
                .ThenInclude(c => c.Category)
            .Include(c => c.CourseTags)
                .ThenInclude(ct => ct.Tags)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();
            CourseDetailVM courseDetailVM = new()
            {
                Course = new Course
                {
                    Desc = course.Desc,
                    CourseImages = course.CourseImages,
                    About = course.About,
                    Apply = course.Apply,
                    Certification = course.Certification,
                },
                Tags = course.CourseTags.Select(c => c.Tags),

                //get categories list with founded id 
                Categories = course.CourseCategories.Select(c => c.Category),

                CourseFeature = course.CourseFeatures,

                //Get Course for header
                CourseCategory = await _context.Category.FirstOrDefaultAsync(c => c.Name == name),
            };
            return View(courseDetailVM);
        }
        public async Task<IActionResult> Search(string value)
        {
            var query = await _context.Course.
                Include(c => c.CourseCategories)
                    .ThenInclude(c => c.Category)
                    .Include(c => c.CourseTags)
                    .ThenInclude(ct => ct.Tags)
                    .Include(c => c.CourseFeatures)
                    .ToListAsync();
            var data = query
                     .Where(c =>
                         c.CourseCategories.Any(cc => cc.Category.Name.ToLower().Contains(value)) ||
                         c.About.ToLower().Contains(value) ||
                         c.CourseTags.Any(ct => ct.Tags.TagName.ToLower().Contains(value)))
                     .Take(9)
                     .ToList();
            return PartialView("_SearchCoursePartialView", data);
        }
    }
}
