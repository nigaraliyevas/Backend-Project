using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.ViewComponents
{
    public class DetailRightSideViewComponent(EduHomeDbContext _context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryCountsAndNames = _context.CourseCategories
            .Include(c => c.Category)
            .GroupBy(c => new { c.CategoryId, c.Category.Name })
            .Select(c => new CategoryNameAndCountVM()
            {
                Name = c.Key.Name,
                Count = c.Count()
            })
            .ToList();

            var theme = _context.Settings.ToDictionary(key => key.Key, value => value.Value);

            var blogs = await _context.Blogs
            .Include(d => d.BlogImages)
            .OrderByDescending(d => d.WrittenDate)
            .Take(3)
            .ToListAsync();
            DetailRightSideVM detailRightSideVM = new()
            {
                CategoryNameAndCountVM = categoryCountsAndNames,
                EducationTheme = theme,
                Blogs = blogs,
            };
            return View(await Task.FromResult(detailRightSideVM));
        }
    }
}
