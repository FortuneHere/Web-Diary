using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDiary.DB.Models
{
    [Table("Lessons")]
    public class Lesson
    {
        public long Id { get; set; }

        public long CourseId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string ClassroomNumber { get; set; }

        public string BuildNumber { get; set; }

        [ForeignKey("CourseId")] public virtual Course Course { get; set; }

        public ICollection<LessonMark> Marks { get; set; }

        public ICollection<Homework> Homeworks { get; set; }
    }
}