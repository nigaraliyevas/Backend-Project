using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.ViewComponents
{
    public class HeaderSettingViewComponent : ViewComponent
    {
        private readonly EduHomeDbContext _context;

        public HeaderSettingViewComponent(EduHomeDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = _context.Settings.ToDictionary(key => key.Key, value => value.Value);
            HeaderVM headerVM = new HeaderVM();
            headerVM.Settings = settings;
            return View(await Task.FromResult(headerVM));
        }
    }
}
