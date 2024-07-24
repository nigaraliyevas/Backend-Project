using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.ViewComponents
{
    public class NoticeViewComponent(EduHomeDbContext _context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notices = await _context.NoticeBoards
            .AsNoTracking()
            .OrderByDescending(n => n.Date)
            .ToListAsync();
            return View(await Task.FromResult(notices));
        }
    }
}
