using EduHome.Models.Common;

namespace EduHome.Models
{
    public class TeacherHobbie : BaseEntity
    {
        public int TeacherId { get; set; }
        public int HobbieId { get; set; }
        public Teacher Teacher { get; set; }
        public Hobbie Hobbie { get; set; }

    }
}
