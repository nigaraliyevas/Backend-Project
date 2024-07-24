using System.ComponentModel.DataAnnotations;
using EduHome.Models.Common;

namespace EduHome.Models
{
    public class CourseImage : BaseEntity
    {
        [MaxLength(50)]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string ImageURL { get; set; }
        [MaxLength(50)]
        public bool IsMain { get; set; }
    }
}
