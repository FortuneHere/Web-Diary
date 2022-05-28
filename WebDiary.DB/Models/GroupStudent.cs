using System.ComponentModel.DataAnnotations.Schema;

namespace WebDiary.DB.Models
{
    [Table("GroupStudents")]
    public class GroupStudent
    {
        public long Id { get; set; }

        public long GroupId { get; set; }

        public long StudentId { get; set; }
        
        [ForeignKey("GroupId")] public virtual Group Group { get; set; }
        
        [ForeignKey("StudentId")] public virtual Student Student { get; set; }

    }
}