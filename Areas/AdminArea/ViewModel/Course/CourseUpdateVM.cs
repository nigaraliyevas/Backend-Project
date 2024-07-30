using EduHome.Models;

namespace EduHome.Areas.AdminArea.ViewModel.Course
{
    public class CourseUpdateVM
    {
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
        public string Assesments { get; set; }
        public double CourseFee { get; set; }
        public List<CourseImage>? CourseImages { get; set; }
        public List<IFormFile>? ImageURLUpload { get; set; }
        public List<int> SelectedCategoryIds { get; set; }
        public List<int> SelectedTagIds { get; set; }
    }
}
