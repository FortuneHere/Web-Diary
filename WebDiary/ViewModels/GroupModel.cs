using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.ViewModels
{
    public class GroupModel
    {
        [Required(ErrorMessage = "Введите номер группы")]
        [DisplayName("Номер")]
        [RegularExpression("\\d{4}", ErrorMessage = "Номер группы должен состоять из 4х чисел")]
        public string Number { get; set; }
        
        [Required(ErrorMessage = "Укажите учебный год")]
        [DisplayName("Учебный год")]
        public int StudyYear { get; set; }
    }
}