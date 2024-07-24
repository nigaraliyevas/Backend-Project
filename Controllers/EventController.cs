using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class EventController(EduHomeDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
            .Include(e => e.EventImages)
            .AsNoTracking()
            .OrderByDescending(d => d.EventStartDate)
            .ToListAsync();
            if (events == null) return NotFound();
            EventVM eventVM = new() { Events = events };
            return View(eventVM);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var ev = await _context.Events
                .Include(e => e.EventSpeaker)
                    .ThenInclude(e => e.Speaker)
                .Include(e => e.EventTags)
                    .ThenInclude(e => e.Tags)
                .Include(e => e.EventImages)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (ev == null) return NotFound();
            EventVM eventVM = new()
            {
                Event = new Event()
                {
                    Name = ev.Name,
                    City = ev.City,
                    FullAddress = ev.FullAddress,
                    EventStartDate = ev.EventStartDate,
                    EventEndDate = ev.EventEndDate,
                    Desc = ev.Desc,
                },
                EventImages = ev.EventImages,
                Speakers = ev.EventSpeaker.Select(es => es.Speaker).ToList(),
                Tags = ev.EventTags.Select(c => c.Tags),
            };
            return View(eventVM);
        }
    }
}
