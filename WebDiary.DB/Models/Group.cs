using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDiary.DB.Models
{
    [Table("Groups")]
    public class Group
    {
        public long Id { get; set; }

        public int StudyYear { get; set; }
        [Required] [MaxLength(4)] public string Number { get; set; }

        public ICollection<GroupStudent> Students { get; set; }
    }
}