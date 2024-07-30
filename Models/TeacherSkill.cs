using EduHome.Models.Common;

namespace EduHome.Models
{
    public class TeacherSkill : BaseEntity
    {
        public int TeacherId { get; set; }
        public int SkillId { get; set; }
        public Teacher Teacher { get; set; }
        public Skill Skill { get; set; }

    }
}
