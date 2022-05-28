using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.ViewModels
{
    public class StudentModel : UserModelBase
    {
        [Required]
        [DisplayName("Группа")]
        [StringLength(4, ErrorMessage = "Номер группы должен содержать 4 символа", MinimumLength = 4)]
        public string GroupNumber { get; set; }
    }
}