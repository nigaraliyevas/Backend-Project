using EduHome.Models.Common;

namespace EduHome.Models
{
    public class EventTag : BaseEntity
    {
        public int EventId { get; set; }
        public int TagId { get; set; }
        public Tag Tags { get; set; }
        public Event Event { get; set; }
    }
}
