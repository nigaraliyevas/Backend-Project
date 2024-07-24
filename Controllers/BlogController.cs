using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class BlogController(EduHomeDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            var blog = await _context.Blogs
            .Include(b => b.BlogImages)
            .Include(b => b.BlogTags)
                .ThenInclude(bt => bt.Tag)
            .FirstOrDefaultAsync(b => b.Id == id);
            BlogVM blogVM = new()
            {
                Blog = new Blog()
                {
                    WrittenBy = blog.WrittenBy,
                    WrittenDate = blog.WrittenDate,
                    Header = blog.Header,
                    Desc = blog.Desc,
                    BlogImages = blog.BlogImages,
                },
                BlogTags = blog.BlogTags.Select(b => b.Tag),
            };
            return View(blogVM);
        }
    }
}
