using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.ViewModels
{
    public class CreateLessonsModel
    {
        public long CourseId { get; set; }
       
        [DisplayName("Название предмета")]
        public string CourseTitle { get; set; }

        [Required(ErrorMessage = "Укажите дату")]
        [DisplayName("Дата первого занятия")]
        public string StartDate { get; set; }
        
        [Required(ErrorMessage = "Укажите дату")]
        [DisplayName("Дата последнего занятия")]
        public string EndDate { get; set; }

        public List<CreateLessonDayModel> SettingsByDay { get; set; }
    }
}