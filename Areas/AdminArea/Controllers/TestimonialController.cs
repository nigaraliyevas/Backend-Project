using EduHome.Areas.AdminArea.ViewModel.Testimonial;
using EduHome.DAL;
using EduHome.Models;
using FiorelloApp.Areas.AdminArea.Extensions;
using FiorelloApp.Areas.AdminArea.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class TestimonialController(EduHomeDbContext _context) : Controller
    {
        // GET: TestimonialController
        public IActionResult Index()
        {
            var testimonial = _context.Testimonials.ToList();
            if (testimonial == null) return NotFound();
            return View(testimonial);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            var testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == id);
            if (testimonial == null) return BadRequest();
            TestimonialDetailVM testimonialDetailVM = new()
            {
                Id = testimonial.Id,
                Name = testimonial.Name,
                Desc = testimonial.Desc,
                ImageURL = testimonial.ImageURL,
                Position = testimonial.Position,
                CreatedDate = testimonial.CreatedDate,
            };
            if (testimonialDetailVM == null) return NotFound();
            return View(testimonialDetailVM);
        }

        // GET: TestimonialController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestimonialController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestimonialCreateVM testimonialCreateVM)
        {
            if (!ModelState.IsValid) return View(testimonialCreateVM);
            var fileImageURL = testimonialCreateVM.ImageURLUpload;
            if (fileImageURL == null) return NotFound();
            if (fileImageURL.Length == 0)
            {
                ModelState.AddModelError("ImageURLUpload", "Can't be empty");
                return View(testimonialCreateVM);
            }
            if (!fileImageURL.CheckContentType("image"))
            {
                ModelState.AddModelError("ImageURLUpload", "Only Image");
                return View(testimonialCreateVM);
            }
            if (fileImageURL.CheckSize(500))
            {
                ModelState.AddModelError("ImageURLUpload", "The Size is big");
                return View(testimonialCreateVM);
            }
            Testimonial testimonial = new Testimonial()
            {
                Name = testimonialCreateVM.Name,
                Desc = testimonialCreateVM.Desc,
                ImageURL = await fileImageURL.SaveFile("testimonial"),
                CreatedDate = DateTime.Now,
                Position = testimonialCreateVM.Position,
            };
            await _context.Testimonials.AddAsync(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: TestimonialController/Edit/5
        public IActionResult Update(int id)
        {
            return View();
        }

        // POST: TestimonialController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TestimonialController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var testimonial = await _context.Testimonials
               .FirstOrDefaultAsync(i => i.Id == id);
            if (testimonial == null) return NotFound();
            Helper.DeleteImageFromFolder(testimonial.ImageURL, "testimonial");
            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
