using EduHome.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class EventImage : BaseEntity
    {
        [MaxLength(50)]
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string ImageURL { get; set; }
        [MaxLength(50)]
        public bool IsMain { get; set; }
    }
}
