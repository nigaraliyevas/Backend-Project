using EduHome.Models;

namespace EduHome.Areas.AdminArea.ViewModel.Course
{
    public class CourseDetailVM
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public string ShortDesc { get; set; }
        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        //features
        public DateTime StartDate { get; set; }
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int StudentCount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Assesments { get; set; }
        public double CourseFee { get; set; }
        public List<CourseImage> CourseImages { get; set; }
        public List<string> CourseTags { get; set; }
        public List<string> CourseCategories { get; set; }
    }
}
