using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Components
{
    public class CourseViewComponent(EduHomeDbContext _context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? take)
        {
            var query = _context.CourseCategories
                .Include(d => d.Course)
                .Include(d => d.Category)
                .Include(d => d.Course.CourseImages)
                .AsQueryable();
            if (take != null) query = query.Take(take.Value);
            var datas = await query.ToListAsync();
            ViewBag.Count = datas.Count();
            return View(await Task.FromResult(datas));
        }
    }
}
