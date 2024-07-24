using EduHome.Models;

namespace EduHome.ViewModels
{
    public class EventVM
    {
        public Event Event { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<EventImage> EventImages { get; set; }
        public IEnumerable<Speaker> Speakers { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
}
