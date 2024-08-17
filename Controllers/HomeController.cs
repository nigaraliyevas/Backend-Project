using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class HomeController(EduHomeDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var datas = await _context.Sliders
                .AsNoTracking()
                .Include(s => s.Content)
                .ToListAsync();

            var noticesRight = await _context.Settings
                .AsNoTracking()
                .ToDictionaryAsync(key => key.Key, value => value.Value);

            var chooseContent = await _context.ChooseContents
                .AsNoTracking()
                .SingleAsync();

            var events = await _context.Events
                .AsNoTracking()
                .OrderByDescending(d => d.EventStartDate)
                .Take(8)
                .ToListAsync();

            if (datas == null || noticesRight == null || chooseContent == null || events == null) return NotFound();

            HomeVM homeVM = new()
            {
                SliderContents = datas,
                NoticeRight = noticesRight,
                ChooseContent = chooseContent,
                Events = events
            };
            return View(homeVM);
        }

    }
}
