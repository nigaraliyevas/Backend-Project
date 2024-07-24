using EduHome.Models.Common;

namespace EduHome.Models
{
    public class EventSpeaker : BaseEntity
    {
        public int SpeakerId { get; set; }
        public int EventId { get; set; }
        public Speaker Speaker { get; set; }
        public Event Event { get; set; }
    }
}
