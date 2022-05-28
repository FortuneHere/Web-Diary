using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDiary.DB.Models
{
    [Table("Courses")]
    public class Course
    {
        public long Id { get; set; }

        public long GroupId { get; set; }

        public long CourseInfoId { get; set; }

        public ClassType ClassType { get; set; }

        public string ClassTypeName
        {
            get
            {
                return ClassType.GetDescription();
            }
        }

        public SemesterType SemesterType { get; set; }

        public long TeacherId { get; set; }

        [ForeignKey("CourseInfoId")] public CourseInfo CourseInfo { get; set; }

        [ForeignKey("GroupId")] public virtual Group Group { get; set; }

        [ForeignKey("TeacherId")] public virtual Teacher Teacher { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }
}