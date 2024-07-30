namespace EduHome.Areas.AdminArea.ViewModel.ChooseContent
{
    public class ChooseContentUpdateVM
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public IFormFile? ImageURLUpload { get; set; }
        public string ImageURL { get; set; }
        public string BgImageURL { get; set; }
        public IFormFile? BgImageURLUpload { get; set; }

    }
}
