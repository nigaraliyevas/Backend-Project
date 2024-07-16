using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class SliderContent : BaseEntity
    {
        [MaxLength(100)]
        public string Header { get; set; }
        [MaxLength(200)]
        public string Subtitle { get; set; }
        [MaxLength(50)]
        public string LinkURL { get; set; }
    }
}
