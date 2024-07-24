using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class ContactController(EduHomeDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View(_context.Settings.ToDictionary(k => k.Key, v => v.Value));
        }
    }
}
