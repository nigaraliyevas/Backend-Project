using EduHome.Models.Common;

namespace EduHome.Models
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public List<TeacherSkill> TeacherSkills { get; set; }

    }
}
