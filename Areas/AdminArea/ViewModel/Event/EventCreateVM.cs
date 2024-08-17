﻿namespace EduHome.Areas.AdminArea.ViewModel.Event
{
    public class EventCreateVM
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string FullAddress { get; set; }
        public string Desc { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public IFormFile[] ImageURLUpload { get; set; }
        public List<int> SelectedSpeakersIds { get; set; }
        public List<int> SelectedTagIds { get; set; }
    }
}