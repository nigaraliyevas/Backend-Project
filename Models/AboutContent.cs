using EduHome.Models.Common;

namespace EduHome.Models
{
    public class AboutContent : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string ImageURL { get; set; }
    }
}
