using EduHome.Models.Common;

namespace EduHome.Models
{
    public class Hobbie : BaseEntity
    {
        public string Name { get; set; }
        public List<TeacherHobbie> TeacherHobbies { get; set; }
    }
}
