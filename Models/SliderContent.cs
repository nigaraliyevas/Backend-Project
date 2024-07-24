using System.ComponentModel.DataAnnotations;
using EduHome.Models.Common;

namespace EduHome.Models
{
    public class SliderContent : BaseEntity
    {
        [MaxLength(100)]
        public string Header { get; set; }
        [MaxLength(200)]
        public string Subtitle { get; set; }
        public int SliderId { get; set; }
        public Slider Slider { get; set; }
    }
}
