using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.ViewComponents
{
    public class FooterSettingViewComponent(EduHomeDbContext _context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footer = _context.Settings.ToDictionary(key => key.Key, value => value.Value);
            return View(await Task.FromResult(footer));
        }
    }
}
