using EduHome.Areas.AdminArea.ViewModel.Event;
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
    public class EventController(EduHomeDbContext _context) : Controller
    {
        // GET: EventController
        public IActionResult Index()
        {
            var evs = _context.Events
                .Include(e => e.EventImages)
                .Include(e => e.EventTags)
                    .ThenInclude(e => e.Tags)
                .Include(e => e.EventSpeaker)
                    .ThenInclude(e => e.Speaker)
                .ToList();
            return View(evs);
        }

        // GET: EventController/Details/5
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            var ev = await _context.Events
                .Include(e => e.EventImages)
                .Include(e => e.EventTags)
                    .ThenInclude(e => e.Tags)
                .Include(e => e.EventSpeaker)
                    .ThenInclude(e => e.Speaker)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (ev == null) return BadRequest();
            EventDetailVM vm = new()
            {
                Id = ev.Id,
                Name = ev.Name,
                City = ev.City,
                FullAddress = ev.FullAddress,
                Desc = ev.Desc,
                EventStartDate = ev.EventStartDate,
                EventEndDate = ev.EventEndDate,
                EventImages = ev.EventImages,
                EventTags = ev.EventTags,
                EventSpeakers = ev.EventSpeaker,
            };
            if (vm == null) return BadRequest();
            return View(vm);
        }
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return NotFound();
            var image = _context.EventImages.FirstOrDefault(i => i.Id == id);
            if (image == null) return NotFound();
            if (image.IsMain) return BadRequest();
            Helper.DeleteImageFromFolder(image.ImageURL, "event");
            _context.EventImages.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Update", new { id = image.EventId });
        }
        public async Task<IActionResult> SetMainPhoto(int? id)
        {
            var image = await _context.EventImages
                .FirstOrDefaultAsync(p => p.Id == id);

            if (id == null) return NotFound();
            if (image == null) return NotFound();
            image.IsMain = true;
            var mainImage = await _context.EventImages.FirstOrDefaultAsync(i => i.IsMain && i.EventId == image.EventId);
            if (mainImage == null) return NotFound();
            mainImage.IsMain = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", new { id = image.EventId });
        }


        // GET: EventController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Speakers = new SelectList(await _context.Speakers.ToListAsync(), "Id", "Name");
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");
            return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateVM eventCreateVM)
        {
            ViewBag.Speakers = new SelectList(await _context.Speakers.ToListAsync(), "Id", "Name");
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");
            if (!ModelState.IsValid) return View(eventCreateVM);

            var fileImageURLs = eventCreateVM.ImageURLUpload;
            Event ev = new Event();
            List<EventImage> list = new List<EventImage>();
            if (fileImageURLs.Count() == 0)
            {
                ModelState.AddModelError("ImageURLUpload", "Can't be empty");
                return View(eventCreateVM);
            }
            foreach (var fileImageURL in fileImageURLs)
            {
                if (fileImageURL == null)
                {
                    ModelState.AddModelError("ImageURLUpload", "Can't be empty");
                    return View(eventCreateVM);
                }
                if (!fileImageURL.CheckContentType("image"))
                {
                    ModelState.AddModelError("ImageURLUpload", "Only Image");
                    return View(eventCreateVM);
                }
                if (fileImageURL.CheckSize(500))
                {
                    ModelState.AddModelError("ImageURLUpload", "The Size is big");
                    return View(eventCreateVM);
                }
                EventImage eventImage = new();
                eventImage.EventId = ev.Id;
                eventImage.ImageURL = await fileImageURL.SaveFile("event");
                if (fileImageURLs[0] == fileImageURL)
                    eventImage.IsMain = true;
                list.Add(eventImage);
            }

            ev.Name = eventCreateVM.Name;
            ev.CreatedDate = DateTime.Now;
            ev.FullAddress = eventCreateVM.FullAddress;
            ev.City = eventCreateVM.City;
            ev.EventStartDate = eventCreateVM.EventStartDate;
            ev.EventEndDate = eventCreateVM.EventEndDate;
            ev.Desc = eventCreateVM.Desc;
            ev.EventImages = list;
            ev.EventSpeaker = eventCreateVM.SelectedSpeakersIds.Select(id => new EventSpeaker { SpeakerId = id }).ToList();
            ev.EventTags = eventCreateVM.SelectedTagIds.Select(id => new EventTag { TagId = id }).ToList();

            await _context.Events.AddAsync(ev);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: EventController/Edit/5
        public async Task<IActionResult> Update(int id)
        {

            if (id == null) return NotFound();
            var existEvent = await _context.Events
                .Include(e => e.EventImages)
                .Include(e => e.EventTags)
                    .ThenInclude(e => e.Tags)
                .Include(e => e.EventSpeaker)
                    .ThenInclude(e => e.Speaker)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (existEvent == null) return NotFound();
            EventUpdateVM eventUpdateVM = new()
            {
                Name = existEvent.Name,
                City = existEvent.City,
                FullAddress = existEvent.FullAddress,
                EventStartDate = existEvent.EventStartDate,
                EventEndDate = existEvent.EventEndDate,
                Desc = existEvent.Desc,
                EventImages = existEvent.EventImages,
            };
            ViewBag.Speakers = new SelectList(await _context.Speakers.ToListAsync(), "Id", "Name");
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");
            return View(eventUpdateVM);
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, EventUpdateVM eventUpdateVM)
        {
            eventUpdateVM.EventImages = await _context.EventImages
                .Where(ci => ci.EventId == id)
                .ToListAsync();
            if (id == null) return NotFound();
            ViewBag.Speakers = new SelectList(await _context.Speakers.ToListAsync(), "Id", "Name");
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");

            if (!ModelState.IsValid)
            {
                return View(eventUpdateVM);
            }
            var existEvent = await _context.Events
                .Include(e => e.EventImages)
                .Include(e => e.EventTags)
                    .ThenInclude(e => e.Tags)
                .Include(e => e.EventSpeaker)
                    .ThenInclude(e => e.Speaker)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (existEvent == null) return NotFound();

            var fileImageURLs = eventUpdateVM.ImageURLUpload;
            if (fileImageURLs != null)
            {
                foreach (var fileImageURL in fileImageURLs)
                {
                    if (!fileImageURL.CheckContentType("image"))
                    {
                        ModelState.AddModelError("ImageURLUpload", "Only Image");
                        return View(eventUpdateVM);
                    }
                    if (fileImageURL.CheckSize(500))
                    {
                        ModelState.AddModelError("ImageURLUpload", "The Size is big");
                        return View(eventUpdateVM);
                    }
                    EventImage eventImage = new();
                    eventImage.EventId = existEvent.Id;
                    eventImage.ImageURL = await fileImageURL.SaveFile("event");
                    eventImage.IsMain = false;
                    existEvent.EventImages.Add(eventImage);

                }
            }

            existEvent.Name = eventUpdateVM.Name;
            existEvent.City = eventUpdateVM.City;
            existEvent.CreatedDate = DateTime.Now;
            existEvent.FullAddress = eventUpdateVM.FullAddress;
            existEvent.Desc = eventUpdateVM.Desc;
            existEvent.EventStartDate = eventUpdateVM.EventStartDate;
            existEvent.EventEndDate = eventUpdateVM.EventEndDate;

            existEvent.EventSpeaker.Clear();
            existEvent.EventSpeaker = eventUpdateVM.SelectedSpeakersIds
                .Select(i => new EventSpeaker { SpeakerId = i })
                .ToList();
            existEvent.EventTags.Clear();
            existEvent.EventTags = eventUpdateVM.SelectedTagIds
                .Select(i => new EventTag { TagId = i })
                .ToList();

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ev = await _context.Events
               .Include(c => c.EventImages)
               .FirstOrDefaultAsync(i => i.Id == id);
            if (ev == null) return NotFound();
            var image = ev.EventImages.FirstOrDefault(i => i.EventId == id);
            if (image == null) return NotFound();
            Helper.DeleteImageFromFolder(image.ImageURL, "event");
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
