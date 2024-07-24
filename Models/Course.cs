using EduHome.Models.Common;

namespace EduHome.Models
{
    public class Course : BaseEntity
    {
        public string Desc { get; set; }
        public string ShortDesc { get; set; }
        public List<CourseImage> CourseImages { get; set; }

        //rest of all belongs when clicked course
        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        //Mant to many relation with category 
        public List<CourseCategory> CourseCategories { get; set; }
        //many to many relation with tag
        public List<CourseTag> CourseTags { get; set; }
        //many to many relation with tag
        public CourseFeature CourseFeatures { get; set; }
    }
}
