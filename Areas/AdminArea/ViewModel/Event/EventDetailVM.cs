using EduHome.Models;

namespace EduHome.Areas.AdminArea.ViewModel.Event
{
    public class EventDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string FullAddress { get; set; }
        public string Desc { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public List<EventImage> EventImages { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
        public List<EventTag> EventTags { get; set; }
    }
}