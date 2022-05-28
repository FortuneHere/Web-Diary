using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.ViewModels
{
    public class UserModelBase
    {
        [Required]
        [StringLength(20, ErrorMessage = "Имя должно быть не более 20 символов")]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Фамилия должна быть не более 20 символов")]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [StringLength(20, ErrorMessage = "Отчество должно быть не более 20 символов")]
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }

        [DisplayName("Логин")]
        [Required]
        [MinLength(3, ErrorMessage = "Логин должно быть не менее 3 символов")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Пароль должно быть не менее 3 символов")]
        public string Password { get; set; }
    }
}