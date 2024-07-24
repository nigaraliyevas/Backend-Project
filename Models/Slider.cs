using EduHome.Models.Common;

namespace EduHome.Models
{
    public class Slider : BaseEntity
    {
        public string ImageURL { get; set; }
        public List<SliderContent> Content { get; set; }
    }
}
