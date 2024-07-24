using System.ComponentModel.DataAnnotations;
using EduHome.Models.Common;

namespace EduHome.Models
{
    public class Setting : BaseEntity
    {
        [MaxLength(100)]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
