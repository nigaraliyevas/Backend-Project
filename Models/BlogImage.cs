using EduHome.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class BlogImage : BaseEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string ImageURL { get; set; }
        [MaxLength(50)]
        public bool IsMain { get; set; }
    }
}
