using System;
using System.Collections.Generic;

namespace WebDiary.ViewModels
{
    public class DairyStudentModel
    {
        public long StudentId { get; set; }
        public string Fio { get; set; }

    }

    public class DairyLessonModel
    {
        public long LessonId { get; set; }
        
        public DateTime Date { get; set; }

        public List<DairyMarkModel> Marks { get; set; }
    }
    
    public class DairyMarkModel
    {
        public long LessonId { get; set; }
        
        public string Mark { get; set; }

        public bool IsAbsent { get; set; }

        public long StudentId { get; set; }

    }
}