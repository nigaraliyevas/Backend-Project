using EduHome.Models;

namespace EduHome.Areas.AdminArea.ViewModel.Teacher
{
    public class TeacherDetailVM
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public List<TeacherImage> TeacherImages { get; set; }
        public TeacherDetail TeacherDetail { get; set; }
        public List<Hobbie> TeacherHobbies { get; set; }
        public List<Skill> TeacherSkills { get; set; }
    }
}
