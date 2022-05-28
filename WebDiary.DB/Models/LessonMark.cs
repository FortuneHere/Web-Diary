using System.ComponentModel.DataAnnotations.Schema;

namespace WebDiary.DB.Models
{
    [Table("LessonMarks")]
    public class LessonMark
    {
        public long Id { get; set; }

        public long LessonId { get; set; }

        public long StudentId { get; set; }

        public bool IsAbsent { get; set; }

        public string Mark { get; set; }

        [ForeignKey("LessonId")] public virtual Lesson Lesson { get; set; }
        [ForeignKey("StudentId")] public virtual Student Student { get; set; }
    }
}