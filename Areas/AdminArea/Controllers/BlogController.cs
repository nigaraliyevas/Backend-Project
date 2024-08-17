using EduHome.Areas.AdminArea.ViewModel.Blog;
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
    public class BlogController(EduHomeDbContext _context) : Controller
    {
        // GET: CategoryController
        public IActionResult Index()
        {
            var blogs = _context.Blogs
                .Include(c => c.BlogImages)
                .Include(c => c.BlogTags)
                    .ThenInclude(c => c.Tag)
                .ToList();
            return View(blogs);
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Detail(int id)
        {
            if (id == null) return NotFound();
            var blog = await _context.Blogs
                .Include(c => c.BlogTags)
                    .ThenInclude(ct => ct.Tag)
                .Include(c => c.BlogImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null) return NotFound();

            BlogDetailVM blogDetailVM = new BlogDetailVM()
            {
                Desc = blog.Desc,
                Header = blog.Header,
                WrittenBy = blog.WrittenBy,
                ImageURL = blog.BlogImages,
                WrittenDate = blog.WrittenDate,
                BlogTags = blog.BlogTags,
            };

            return View(blogDetailVM);
        }

        // GET: CategoryController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blogCreateVM)
        {
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");

            if (!ModelState.IsValid) return View(blogCreateVM);
            var fileImageURLs = blogCreateVM.ImageURLUpload;
            Blog blog = new Blog();
            List<BlogImage> list = new List<BlogImage>();
            if (fileImageURLs.Count() == 0)
            {
                ModelState.AddModelError("ImageURLUpload", "Can't be empty");
                return View(blogCreateVM);
            }
            foreach (var fileImageURL in fileImageURLs)
            {
                if (fileImageURL == null)
                {
                    ModelState.AddModelError("ImageURLUpload", "Can't be empty");
                    return View(blogCreateVM);
                }
                if (!fileImageURL.CheckContentType("image"))
                {
                    ModelState.AddModelError("ImageURLUpload", "Only Image");
                    return View(blogCreateVM);
                }
                if (fileImageURL.CheckSize(500))
                {
                    ModelState.AddModelError("ImageURLUpload", "The Size is big");
                    return View(blogCreateVM);
                }
                BlogImage blogImage = new();
                blogImage.BlogId = blog.Id;
                blogImage.ImageURL = await fileImageURL.SaveFile("blog");
                if (fileImageURLs[0] == fileImageURL)
                    blogImage.IsMain = true;
                list.Add(blogImage);

            }

            blog.Desc = blogCreateVM.Desc;
            blog.Header = blogCreateVM.Header;
            blog.WrittenBy = blogCreateVM.WrittenBy;
            blog.WrittenDate = blogCreateVM.WrittenDate;
            blog.CreatedDate = DateTime.Now;
            blog.BlogImages = list;
            blog.BlogTags = blogCreateVM.SelectedTagIds.Select(id => new BlogTag { TagId = id }).ToList();

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            var existBlog = await _context.Blogs
                .Include(c => c.BlogImages)
                .Include(c => c.BlogTags)
                    .ThenInclude(c => c.Tag)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (existBlog == null) return NotFound();
            BlogUpdateVM blogUpdateVM = new()
            {
                Desc = existBlog.Desc,
                Header = existBlog.Header,
                WrittenBy = existBlog.WrittenBy,
                WrittenDate = existBlog.WrittenDate,
                CreatedDate = DateTime.Now,
                BlogImages = existBlog.BlogImages,
            };
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");

            return View(blogUpdateVM);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, BlogUpdateVM blogUpdateVM)
        {
            if (id == null) return NotFound();
            blogUpdateVM.BlogImages = await _context.BlogImages
                     .Where(ci => ci.BlogId == id)
                     .ToListAsync();
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "TagName");

            if (!ModelState.IsValid) return View(blogUpdateVM);
            var existBlog = await _context.Blogs
               .Include(c => c.BlogTags)
                   .ThenInclude(c => c.Tag)
               .Include(c => c.BlogImages)
               .FirstOrDefaultAsync(c => c.Id == id);
            if (existBlog == null) return NotFound();

            var fileImageURLs = blogUpdateVM.ImageURLUpload;
            if (fileImageURLs != null)
            {
                foreach (var fileImageURL in fileImageURLs)
                {
                    if (!fileImageURL.CheckContentType("image"))
                    {
                        var images = await _context.BlogImages
                         .Where(ci => ci.BlogId == existBlog.Id)
                         .ToListAsync();
                        blogUpdateVM.BlogImages = images;
                        ModelState.AddModelError("ImageURLUpload", "Only Image");
                        return View(blogUpdateVM);
                    }
                    if (fileImageURL.CheckSize(500))
                    {
                        var images = await _context.BlogImages
                         .Where(ci => ci.BlogId == existBlog.Id)
                         .ToListAsync();
                        blogUpdateVM.BlogImages = images;
                        ModelState.AddModelError("ImageURLUpload", "The Size is big");
                        return View(blogUpdateVM);
                    }
                    BlogImage blogImage = new();
                    blogImage.BlogId = existBlog.Id;
                    blogImage.ImageURL = await fileImageURL.SaveFile("Blog");
                    blogImage.IsMain = false;
                    existBlog.BlogImages.Add(blogImage);

                }
            }

            existBlog.Desc = blogUpdateVM.Desc;
            existBlog.Header = blogUpdateVM.Header;
            existBlog.CreatedDate = DateTime.Now;
            existBlog.WrittenBy = blogUpdateVM.WrittenBy;
            existBlog.WrittenDate = blogUpdateVM.WrittenDate;
            existBlog.BlogTags = blogUpdateVM.SelectedTagIds.Select(id => new BlogTag { TagId = id }).ToList();
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return NotFound();
            var image = _context.BlogImages.FirstOrDefault(i => i.Id == id);
            if (image == null) return NotFound();
            if (image.IsMain) return BadRequest();
            Helper.DeleteImageFromFolder(image.ImageURL, "blog");
            _context.BlogImages.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Update", new { id = image.BlogId });
        }

        // POST: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var blog = await _context.Blogs
               .Include(c => c.BlogImages)
               .FirstOrDefaultAsync(i => i.Id == id);
            if (blog == null) return NotFound();
            var image = blog.BlogImages.FirstOrDefault(i => i.BlogId == id);
            if (image == null) return NotFound();
            Helper.DeleteImageFromFolder(image.ImageURL, "blog");
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
