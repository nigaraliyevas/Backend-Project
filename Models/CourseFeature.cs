﻿using EduHome.Models.Common;

namespace EduHome.Models
{
    public class CourseFeature : BaseEntity
    {
        //one to one relation with Course table
        public int CourseId { get; set; }
        public Course Course { get; set; }//navigation prop
        public DateTime StartDate { get; set; }
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int StudentCount { get; set; }
        public string Assesments { get; set; }
        public double CourseFee { get; set; }
    }
}
