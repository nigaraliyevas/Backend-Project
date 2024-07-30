using EduHome.Areas.AdminArea.ViewModel.Course;
using EduHome.DAL;
using EduHome.Models;
using FiorelloApp.Areas.AdminArea.Extensions;
using FiorelloApp.Areas.AdminArea.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CourseController(EduHomeDbContext _context) : Controller
    {
        // GET: CategoryController
        public ActionResult Index()
        {
            var courses = _context.Course
                .Include(c => c.CourseTags)
                    .ThenInclude(c => c.Tags)
                .Include(c => c.CourseImages)
                .Include(c => c.CourseCategories)
                    .ThenInclude(c => c.Category)
                .Include(c => c.CourseFeatures)
                .ToList();
            return View(courses);
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> Detail(int id)
        {
            if (id == null) return NotFound();
            var course = await _context.Course
                .Include(c => c.CourseTags)
                    .ThenInclude(ct => ct.Tags)
                .Include(c => c.CourseImages)
                .Include(c => c.CourseCategories)
                    .ThenInclude(cc => cc.Category)
                .Include(c => c.CourseFeatures)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null) return NotFound();

            CourseDetailVM courseDetailVM = new CourseDetailVM
            {
                Id = course.Id,
                Desc = course.Desc,
                ShortDesc = course.ShortDesc,
                StartDate = course.CourseFeatures.StartDate,
                Duration = course.CourseFeatures.Duration,
                ClassDuration = course.CourseFeatures.ClassDuration,
                SkillLevel = course.CourseFeatures.SkillLevel,
                Language = course.CourseFeatures.Language,
                CreatedDate = course.CreatedDate,
                StudentCount = course.CourseFeatures.StudentCount,
                Assesments = course.CourseFeatures.Assesments,
                CourseFee = course.CourseFeatures.CourseFee,
                CourseCategories = course.CourseCategories.Select(cc => cc.Category.Name).ToList(),
                CourseTags = course.CourseTags.Select(ct => ct.Tags.TagName).ToList(),
                CourseImages = course.CourseImages.ToList()
            };

            return View(courseDetailVM);
        }

        // GET: CategoryController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _context.Category.ToListAsync(), "Id", "Name");
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CourseCreateVM courseCreateVM)
        {
            ViewBag.Categories = new SelectList(await _context.Category.ToListAsync(), "Id", "Name");
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");

            if (!ModelState.IsValid) return View(courseCreateVM);
            var fileImageURLs = courseCreateVM.ImageURLUpload;
            Course course = new Course();
            List<CourseImage> list = new List<CourseImage>();
            if (fileImageURLs.Count == 0)
            {
                ModelState.AddModelError("UploadPhotos", "Can't be empty");
                return View(courseCreateVM);
            }
            foreach (var fileImageURL in fileImageURLs)
            {
                if (fileImageURL == null)
                {
                    ModelState.AddModelError("Photo", "Can't be empty");
                    return View(courseCreateVM);
                }
                if (!fileImageURL.CheckContentType("image"))
                {
                    ModelState.AddModelError("Photo", "Only Image");
                    return View(courseCreateVM);
                }
                if (fileImageURL.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "The Size is big");
                    return View(courseCreateVM);
                }
                CourseImage courseImage = new();
                courseImage.CourseId = course.Id;
                courseImage.ImageURL = await fileImageURL.SaveFile("course");
                if (fileImageURLs[0] == fileImageURL)
                    courseImage.IsMain = true;
                list.Add(courseImage);

            }

            course.Desc = courseCreateVM.Desc;
            course.ShortDesc = courseCreateVM.ShortDesc;
            course.CourseImages = list;
            course.CreatedDate = DateTime.Now;
            course.Apply = courseCreateVM.Apply;
            course.About = courseCreateVM.About;
            course.CourseFeatures = new()
            {
                StartDate = courseCreateVM.StartDate,
                Duration = courseCreateVM.Duration,
                ClassDuration = courseCreateVM.ClassDuration,
                SkillLevel = courseCreateVM.SkillLevel,
                Language = courseCreateVM.Language,
                StudentCount = courseCreateVM.StudentCount,
                Assesments = courseCreateVM.Assesments,
                CourseFee = courseCreateVM.CourseFee,
            };
            course.Certification = courseCreateVM.Certification;
            course.CourseCategories = courseCreateVM.SelectedCategoryIds.Select(id => new CourseCategory { CategoryId = id }).ToList();
            course.CourseTags = courseCreateVM.SelectedTagIds.Select(id => new CourseTag { TagId = id }).ToList();

            await _context.Course.AddAsync(course);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            var existCourse = await _context.Course
                .Include(c => c.CourseTags)
                    .ThenInclude(c => c.Tags)
                .Include(c => c.CourseImages)
                .Include(c => c.CourseCategories)
                    .ThenInclude(c => c.Category)
                .Include(c => c.CourseFeatures)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (existCourse == null) return NotFound();
            CourseUpdateVM courseUpdateVM = new()
            {
                Desc = existCourse.Desc,
                ShortDesc = existCourse.ShortDesc,
                About = existCourse.About,
                Apply = existCourse.Apply,
                Certification = existCourse.Certification,
                StartDate = existCourse.CourseFeatures.StartDate,
                Duration = existCourse.CourseFeatures.Duration,
                ClassDuration = existCourse.CourseFeatures.ClassDuration,
                SkillLevel = existCourse.CourseFeatures.SkillLevel,
                Language = existCourse.CourseFeatures.Language,
                StudentCount = existCourse.CourseFeatures.StudentCount,
                Assesments = existCourse.CourseFeatures.Assesments,
                CourseFee = existCourse.CourseFeatures.CourseFee,
                CourseImages = existCourse.CourseImages
            };
            ViewBag.Categories = new SelectList(await _context.Category.ToListAsync(), "Id", "Name");
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");

            return View(courseUpdateVM);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int? id, CourseUpdateVM courseUpdateVM)
        {
            ViewBag.Categories = new SelectList(await _context.Category.ToListAsync(), "Id", "Name");
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");

            if (!ModelState.IsValid) return View(courseUpdateVM);
            if (id == null) return NotFound();
            var existCourse = await _context.Course
               .Include(c => c.CourseTags)
                   .ThenInclude(c => c.Tags)
               .Include(c => c.CourseImages)
               .Include(c => c.CourseCategories)
                   .ThenInclude(c => c.Category)
               .Include(c => c.CourseFeatures)
               .FirstOrDefaultAsync(c => c.Id == id);
            if (existCourse == null) return NotFound();

            var fileImageURLs = courseUpdateVM.ImageURLUpload;

            if (fileImageURLs.Count() == 0)
            {
                ModelState.AddModelError("ImageURLUpload", "Can't be empty");
                return View(courseUpdateVM);
            }
            foreach (var fileImageURL in fileImageURLs)
            {
                if (fileImageURL == null)
                {
                    ModelState.AddModelError("ImageURLUpload", "Can't be empty");
                    return View(courseUpdateVM);
                }
                if (!fileImageURL.CheckContentType("image/"))
                {
                    ModelState.AddModelError("ImageURLUpload", "Only Image");
                    return View(courseUpdateVM);
                }
                if (fileImageURL.CheckSize(500))
                {
                    ModelState.AddModelError("ImageURLUpload", "The Size is big");
                    return View(courseUpdateVM);
                }
                CourseImage courseImage = new();
                courseImage.CourseId = existCourse.Id;
                courseImage.ImageURL = await fileImageURL.SaveFile("course");
                courseImage.IsMain = false;
                existCourse.CourseImages.Add(courseImage);

            }

            existCourse.Desc = courseUpdateVM.Desc;
            existCourse.ShortDesc = courseUpdateVM.ShortDesc;
            existCourse.CreatedDate = DateTime.Now;
            existCourse.Apply = courseUpdateVM.Apply;
            existCourse.About = courseUpdateVM.About;
            existCourse.CourseFeatures = new()
            {
                StartDate = courseUpdateVM.StartDate,
                Duration = courseUpdateVM.Duration,
                ClassDuration = courseUpdateVM.ClassDuration,
                SkillLevel = courseUpdateVM.SkillLevel,
                Language = courseUpdateVM.Language,
                StudentCount = courseUpdateVM.StudentCount,
                Assesments = courseUpdateVM.Assesments,
                CourseFee = courseUpdateVM.CourseFee,
            };
            existCourse.Certification = courseUpdateVM.Certification;
            existCourse.CourseCategories = courseUpdateVM.SelectedCategoryIds.Select(id => new CourseCategory { CategoryId = id }).ToList();
            existCourse.CourseTags = courseUpdateVM.SelectedTagIds.Select(id => new CourseTag { TagId = id }).ToList();
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return NotFound();
            var image = _context.CourseImages.FirstOrDefault(i => i.Id == id);
            if (image == null) return NotFound();
            if (image.IsMain) return BadRequest();
            Helper.DeleteImageFromFolder(image.ImageURL, "course");
            _context.CourseImages.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Update", new { id = image.CourseId });
        }
        public async Task<IActionResult> SetMainPhoto(int? id)
        {
            var image = await _context.CourseImages
                .FirstOrDefaultAsync(p => p.Id == id);

            if (id == null) return NotFound();
            if (image == null) return NotFound();
            image.IsMain = true;
            var mainImage = await _context.CourseImages.FirstOrDefaultAsync(i => i.IsMain && i.CourseId == image.CourseId);
            mainImage.IsMain = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", new { id = image.CourseId });
        }

        // POST: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            //if (id == null) return NotFound();
            //var course = await _context.Course
            //   .Include(c => c.CourseImages)
            //   .FirstOrDefaultAsync(i => i.Id == id);
            //if (course == null) return NotFound();
            //var image = course.CourseImages.FirstOrDefault(i => i.CourseId == id);
            //if (image == null) return NotFound();
            //Helper.DeleteImageFromFolder("choose", image.ImageURL);
            //_context.CourseImages.Remove(image);
            //await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
