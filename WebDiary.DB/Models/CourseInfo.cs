using System.ComponentModel.DataAnnotations.Schema;

namespace WebDiary.DB.Models
{
    [Table("CourseInfos")]
    public class CourseInfo
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Department { get; set; }
    }
}