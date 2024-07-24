using System.ComponentModel.DataAnnotations;
using EduHome.Models.Common;

namespace EduHome.Models
{
    public class ChooseContent : BaseEntity
    {
        [MaxLength(200)]
        public string Header { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string BgImageURL { get; set; }
        //home blue area includes a student
    }
}
