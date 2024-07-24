using EduHome.Models.Common;

namespace EduHome.Models
{
    public class CourseTag : BaseEntity
    {
        public int CourseId { get; set; }
        public int TagId { get; set; }
        public Tag Tags { get; set; }
        public Course Course { get; set; }
    }
}
