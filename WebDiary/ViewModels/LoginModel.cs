using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.ViewModels
{
    public class LoginModel
    {
        [Required]
        [DisplayName("Логин")]
        [MinLength(3, ErrorMessage = "Логин должно быть не менее 3 символов")]
        public string Login { get; set; }

        [Required]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Пароль должно быть не менее 3 символов")]
        public string Password { get; set; }
    }
}