using EduHome.Models.Common;
namespace EduHome.Models
{
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }
        public List<CourseTag> CourseTags { get; set; }
        public List<EventTag> EventTags { get; set; }
        public List<BlogTag> BlogTags { get; set; }
    }
}
