using EduHome.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class Speaker : BaseEntity
    {
        [MaxLength(80)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Position { get; set; }
        public string ImageURL { get; set; }
        public List<EventSpeaker> EventSpeaker { get; set; }
    }
}
