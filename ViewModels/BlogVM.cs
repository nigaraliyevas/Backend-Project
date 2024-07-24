using EduHome.Models;

namespace EduHome.ViewModels
{
    public class BlogVM
    {
        public Blog Blog { get; set; }
        public IEnumerable<Tag> BlogTags { get; set; }
    }
}
