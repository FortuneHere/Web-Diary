using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDiary.DB.Models
{
    [Table("Students")]
    public class Student
    {
        public long Id { get; set; }

        public User User { get; set; }

        public ICollection<GroupStudent> Groups { get; set; }

        public ICollection<LessonMark> Marks { get; set; }
    }
}