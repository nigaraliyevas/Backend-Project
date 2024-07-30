using EduHome.DAL.Configurations;
using EduHome.Models;
using Microsoft.EntityFrameworkCore;

namespace EduHome.DAL
{
    public class EduHomeDbContext : DbContext
    {
        public EduHomeDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderContent> SliderContent { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<ChooseContent> ChooseContents { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseFeature> CourseFeatures { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<EventTag> EventTags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<AboutContent> AboutContents { get; set; }
        //Teacher
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<TeacherImage> TeacherImages { get; set; }
        public DbSet<TeacherDetail> TeacherDetails { get; set; }
        public DbSet<Hobbie> Hobbies { get; set; }
        public DbSet<TeacherHobbie> TeacherHobbies { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TeacherSkill> TeacherSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SettingConfirguration());
        }
    }
}
