namespace EduHome.Areas.AdminArea.ViewModel.Blog
{
    public class BlogCreateVM
    {
        public string WrittenBy { get; set; }
        public DateTime WrittenDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Header { get; set; }
        public string Desc { get; set; }
        public List<IFormFile> ImageURLUpload { get; set; }
        public List<int> SelectedTagIds { get; set; }
    }
}
