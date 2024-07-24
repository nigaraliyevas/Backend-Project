using EduHome.Models.Common;

namespace EduHome.Models
{
    public class Blog : BaseEntity
    {
        public string WrittenBy { get; set; }
        public DateTime WrittenDate { get; set; }
        public string Header { get; set; }
        public string Desc { get; set; }
        public List<BlogTag> BlogTags { get; set; }
        public List<BlogImage> BlogImages { get; set; }
    }
}
