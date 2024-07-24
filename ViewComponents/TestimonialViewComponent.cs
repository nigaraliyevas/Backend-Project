using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.ViewComponents
{
    public class TestimonialViewComponent(EduHomeDbContext _context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonial = _context.Testimonials.Single();
            return View(await Task.FromResult(testimonial));
        }
    }
}