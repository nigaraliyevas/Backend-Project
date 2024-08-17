using EduHome.Models;

namespace EduHome.Areas.AdminArea.ViewModel.Blog
{
    public class BlogDetailVM
    {
        public string WrittenBy { get; set; }
        public DateTime WrittenDate { get; set; }
        public string Header { get; set; }
        public string Desc { get; set; }
        public List<BlogImage> ImageURL { get; set; }
        public List<BlogTag> BlogTags { get; set; }
    }
}
