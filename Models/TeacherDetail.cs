using EduHome.Models.Common;

namespace EduHome.Models
{
    public class TeacherDetail : BaseEntity
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string About { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Faculty { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public string SkypeUsername { get; set; }

    }
}
