using EduHome.Models.Common;

namespace EduHome.Models
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public List<TeacherImage> TeacherImages { get; set; }
        public TeacherDetail TeacherDetail { get; set; }
        public List<TeacherHobbie> TeacherHobbies { get; set; }
        public List<TeacherSkill> TeacherSkills { get; set; }

    }
}
