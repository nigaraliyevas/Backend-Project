using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.ViewComponents
{
    public class BlogViewComponent(EduHomeDbContext _context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? take)
        {
            var query = _context.Blogs
                .Include(d => d.BlogImages)
                .OrderByDescending(d => d.WrittenDate)
                .AsQueryable();
            if (take != null) query = query.Take(take.Value);
            var datas = await query.ToListAsync();
            ViewBag.Count = datas.Count;
            return View(await Task.FromResult(datas));
        }
    }
}
