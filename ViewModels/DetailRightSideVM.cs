using EduHome.Models;

namespace EduHome.ViewModels
{
    public class DetailRightSideVM
    {
        public IEnumerable<CategoryNameAndCountVM> CategoryNameAndCountVM { get; set; }
        public Dictionary<string, string> EducationTheme { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
