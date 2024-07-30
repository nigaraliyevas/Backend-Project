using EduHome.Models;

namespace EduHome.ViewModels
{
    public class TeacherVM
    {
        public Teacher Teacher { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
    }
}
