using System.Collections.Generic;

namespace WebDiary.DB.Models
{
    public class Teacher
    {
        public long Id { get; set; }

        public bool IsAdministrator { get; set; }

        public User User { get; set; }

        public string Post { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}