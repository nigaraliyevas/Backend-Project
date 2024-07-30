using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class NoticeController(EduHomeDbContext _context) : Controller
    {
        // GET
        public async Task<ActionResult> Index()
        {
            return View(await _context.NoticeBoards.ToListAsync());
        }

        // GET
        public async Task<ActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();

            var noticeBoard = await _context.NoticeBoards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeBoard == null) return NotFound();

            return View(noticeBoard);
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Desc")] NoticeBoard noticeBoard)
        {
            if (!ModelState.IsValid) return View(noticeBoard);
            if (noticeBoard == null) return BadRequest();
            noticeBoard.CreatedDate = DateTime.Now;
            _context.Add(noticeBoard);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        // GET
        public async Task<ActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            var noticeBoard = await _context.NoticeBoards.FirstOrDefaultAsync(n => n.Id == id);
            if (noticeBoard == null) return NotFound();
            return View(noticeBoard);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int? id, [Bind("Date,Desc")] NoticeBoard noticeBoard)
        {
            if (!ModelState.IsValid) return View(noticeBoard);
            var existNotice = await _context.NoticeBoards.FirstOrDefaultAsync(n => n.Id == id);
            if (existNotice == null) return NotFound();
            existNotice.CreatedDate = DateTime.Now;
            existNotice.Desc = noticeBoard.Desc;
            existNotice.Date = noticeBoard.Date;
            _context.Update(existNotice);
            await _context.SaveChangesAsync();

            return RedirectToAction("index", "notice");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            var noticeBoard = await _context.NoticeBoards.FirstOrDefaultAsync(n => n.Id == id);
            _context.NoticeBoards.Remove(noticeBoard);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
    }
}
