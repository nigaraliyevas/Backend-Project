using EduHome.Models.Common;

namespace EduHome.Models
{
    public class TeacherImage : BaseEntity
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string ImageURL { get; set; }
        public bool IsMain { get; set; }

    }
}
