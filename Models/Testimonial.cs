using EduHome.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class Testimonial : BaseEntity
    {
        public string ImageURL { get; set; }
        public string Desc { get; set; }
        [MaxLength(80)]
        public string Name { get; set; }
        [MaxLength(80)]
        public string Position { get; set; }
    }
}
