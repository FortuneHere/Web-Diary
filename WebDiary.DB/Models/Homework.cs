using System.ComponentModel.DataAnnotations.Schema;

namespace WebDiary.DB.Models
{
    [Table("Homeworks")]
    public class Homework
    {
        public long Id { get; set; }

        public long LessonId { get; set; }

        public string Text { get; set; }
    }
}