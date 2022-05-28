using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.ViewModels
{
    public class CourseInfoModel
    {
        [Required(ErrorMessage = "Введите название предмета")]
        [DisplayName("Название")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Введите название факультета")]
        [DisplayName("Факультет")]
        public string Department { get; set; }
    }
}