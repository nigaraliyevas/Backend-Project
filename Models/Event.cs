using EduHome.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class Event : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(200)]
        public string FullAddress { get; set; }
        public string Desc { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }

        //Many to many relation with speaker
        public List<EventSpeaker> EventSpeaker { get; set; }

        //Many to many with tags
        public List<EventTag> EventTags { get; set; }

        public List<EventImage> EventImages { get; set; }

    }
}
