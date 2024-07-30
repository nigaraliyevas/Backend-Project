namespace EduHome.Areas.AdminArea.ViewModel.ChooseContent
{
    public class ChooseContentCreateVM
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public IFormFile ImageURLUpload { get; set; }
        public IFormFile BgImageURLUpload { get; set; }

    }
}
