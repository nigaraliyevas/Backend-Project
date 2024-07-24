using System.ComponentModel.DataAnnotations;
using EduHome.Models.Common;

namespace EduHome.Models
{
    public class NoticeBoard : BaseEntity
    {
        public DateTime Date { get; set; }
        [MaxLength(150)]
        public string Desc { get; set; }
    }
}
