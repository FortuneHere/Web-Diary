using System.Collections.Generic;
using System.ComponentModel;

namespace WebDiary.ViewModels
{
    public class EditDairyModel
    {
        public long CourseId { get; set; }

        [DisplayName("Номер группы")] public string GroupNumber { get; set; }

        [DisplayName("Название предмета")] public string CourseTitle { get; set; }

        public List<DairyStudentModel> Students { get; set; }

        public List<DairyLessonModel> Lessons { get; set; }
    }
}