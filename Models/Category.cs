using EduHome.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class Category : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        //Many to may with Course
        public List<CourseCategory> CourseCategories { get; set; }
    }
}
