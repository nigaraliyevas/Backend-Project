using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class AboutController(EduHomeDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var about = await _context.AboutContents.SingleAsync();
            var videoLink = _context.Settings.ToDictionary(key => key.Key, value => value.Value);
            if (about == null || videoLink == null) return NotFound();
            AboutVM aboutVM = new()
            {
                AboutContent = about,
                VideoLink = videoLink,
            };
            return View(aboutVM);
        }
    }
}
