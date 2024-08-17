using EduHome.Models;

namespace EduHome.Areas.AdminArea.ViewModel.Blog
{
    public class BlogUpdateVM
    {
        public string WrittenBy { get; set; }
        public DateTime WrittenDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Header { get; set; }
        public string Desc { get; set; }
        public List<int> SelectedTagIds { get; set; }
        public List<IFormFile> ImageURLUpload { get; set; }
        public List<BlogImage>? BlogImages { get; set; }
    }
}
