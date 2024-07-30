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
        public ActionResult Index()
        {
            var content = _context.ChooseContents.ToList();
            return View(content);
        }

        // GET
        public async Task<ActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();

            var chooseContent = await _context.ChooseContents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chooseContent == null) return NotFound();

            return View(chooseContent);
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChooseContentCreateVM chooseContentCreateVM)
        {
            if (!ModelState.IsValid) return View(chooseContentCreateVM);
            var fileImageURL = chooseContentCreateVM.ImageURLUpload;
            var fileBgImageURL = chooseContentCreateVM.BgImageURLUpload;
            if (fileImageURL == null || fileBgImageURL == null)
            {
                ModelState.AddModelError("Photo", "Can't be empty");
                return View(chooseContentCreateVM);
            }
            if (!fileImageURL.CheckContentType("image") || !fileBgImageURL.CheckContentType("image"))
            {
                ModelState.AddModelError("Photo", "Only Image");
                return View(chooseContentCreateVM);
            }
            if (fileImageURL.CheckSize(500) || fileBgImageURL.CheckSize(700))
            {
                ModelState.AddModelError("Photo", "The Size is big");
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
        public ActionResult Update(int? id)
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
        public async Task<ActionResult> Update(int id, ChooseContentUpdateVM chooseContentUpdateVM)
        {
            if (!ModelState.IsValid) return View(chooseContentUpdateVM);
            if (id == null) return NotFound();
            var existContent = _context.ChooseContents.FirstOrDefault(d => d.Id == id);
            var fileImageURL = chooseContentUpdateVM.ImageURLUpload;
            var fileBgImageURL = chooseContentUpdateVM.BgImageURLUpload;
            if (fileImageURL == null || fileBgImageURL == null)
            {
                ModelState.AddModelError("Photo", "Can't be empty");
                return View(chooseContentUpdateVM);
            }
            if (!fileImageURL.CheckContentType("image") || !fileBgImageURL.CheckContentType("image"))
            {
                ModelState.AddModelError("Photo", "Only Image");
                return View(chooseContentUpdateVM);
            }
            if (fileImageURL.CheckSize(500) || fileBgImageURL.CheckSize(700))
            {
                ModelState.AddModelError("Photo", "The Size is big");
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
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var image = _context.ChooseContents.FirstOrDefault(i => i.Id == id);
            if (image == null) return NotFound();
            Helper.DeleteImageFromFolder("choose", image.ImageURL);
            Helper.DeleteImageFromFolder("choose", image.BgImageURL);
            _context.ChooseContents.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
