using EduHome.Areas.AdminArea.ViewModel.ChooseContent;
using EduHome.DAL;
using EduHome.Models;
using FiorelloApp.Areas.AdminArea.Extensions;
using FiorelloApp.Areas.AdminArea.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ChooseContentController(EduHomeDbContext _context) : Controller
    {
        // GET
        public IActionResult Index()
        {
            var content = _context.ChooseContents.ToList();
            return View(content);
        }

        // GET
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();

            var chooseContent = await _context.ChooseContents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chooseContent == null) return NotFound();

            return View(chooseContent);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChooseContentCreateVM chooseContentCreateVM)
        {
            if (!ModelState.IsValid) return View(chooseContentCreateVM);
            var fileImageURL = chooseContentCreateVM.ImageURLUpload;
            var fileBgImageURL = chooseContentCreateVM.BgImageURLUpload;
            if (fileImageURL == null || fileBgImageURL == null)
            {
                ModelState.AddModelError("ImageURLUpload", "Can't be empty");
                return View(chooseContentCreateVM);
            }
            if (!fileImageURL.CheckContentType("image") || !fileBgImageURL.CheckContentType("image"))
            {
                ModelState.AddModelError("ImageURLUpload", "Only Image");
                return View(chooseContentCreateVM);
            }
            if (fileImageURL.CheckSize(1000))
            {
                ModelState.AddModelError("ImageURLUpload", "The Size is big");
                return View(chooseContentCreateVM);
            }
            if (fileBgImageURL.CheckSize(1000))
            {
                ModelState.AddModelError("BgImageURLUpload", "The Size is big");
                return View(chooseContentCreateVM);
            }
            ChooseContent chooseContent = new ChooseContent()
            {
                Header = chooseContentCreateVM.Header,
                Description = chooseContentCreateVM.Description,
                ImageURL = await fileImageURL.SaveFile("choose"),
                CreatedDate = DateTime.Now,
                BgImageURL = await fileBgImageURL.SaveFile("choose")
            };
            await _context.ChooseContents.AddAsync(chooseContent);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var existContent = _context.ChooseContents.FirstOrDefault(d => d.Id == id);
            ChooseContentUpdateVM chooseContentUpdateVM = new()
            {
                Header = existContent.Header,
                Description = existContent.Description,
                BgImageURL = existContent.BgImageURL,
                ImageURL = existContent.ImageURL,
            };
            return View(chooseContentUpdateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, ChooseContentUpdateVM chooseContentUpdateVM)
        {
            if (id == null) return NotFound();
            var existContent = _context.ChooseContents.FirstOrDefault(d => d.Id == id);
            if (existContent == null) return NotFound();

            if (!ModelState.IsValid)
            {
                chooseContentUpdateVM.ImageURL = existContent.ImageURL;
                chooseContentUpdateVM.BgImageURL = existContent.BgImageURL;
                return View(chooseContentUpdateVM);
            }
            var fileImageURL = chooseContentUpdateVM.ImageURLUpload;
            var fileBgImageURL = chooseContentUpdateVM.BgImageURLUpload;
            if (!fileImageURL.CheckContentType("image") || !fileBgImageURL.CheckContentType("image"))
            {
                ModelState.AddModelError("ImageURLUpload", "Only Image");
                ModelState.AddModelError("BgImageURLUpload", "Only Image");
                return View(chooseContentUpdateVM);
            }
            if (fileImageURL.CheckSize(500) || fileBgImageURL.CheckSize(700))
            {
                ModelState.AddModelError("ImageURLUpload", "The Size is big");
                ModelState.AddModelError("BgImageURLUpload", "The Size is big");
                return View(chooseContentUpdateVM);
            }

            existContent.Header = chooseContentUpdateVM.Header;
            existContent.Description = chooseContentUpdateVM.Description;
            existContent.ImageURL = await fileImageURL.SaveFile("choose");
            existContent.CreatedDate = DateTime.Now;
            existContent.BgImageURL = await fileBgImageURL.SaveFile("choose");
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var image = _context.ChooseContents.FirstOrDefault(i => i.Id == id);
            if (image == null) return NotFound();
            Helper.DeleteImageFromFolder(image.ImageURL, "choose");
            Helper.DeleteImageFromFolder(image.BgImageURL, "choose");
            _context.ChooseContents.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
