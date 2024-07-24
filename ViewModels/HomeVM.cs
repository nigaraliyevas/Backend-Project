using EduHome.Models;

namespace EduHome.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> SliderContents { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public Dictionary<string, string> NoticeRight { get; set; }
        public ChooseContent ChooseContent { get; set; }
    }
}
