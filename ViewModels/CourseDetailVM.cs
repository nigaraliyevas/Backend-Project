using EduHome.Models;

namespace EduHome.ViewModels
{
    public class CourseDetailVM
    {
        public Course Course { get; set; }
        public Category CourseCategory { get; set; }
        public CourseFeature CourseFeature { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        //todo add blogs type will be list

    }
}
