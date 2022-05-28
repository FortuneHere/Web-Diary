using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebDiary.DB.Models
{
    [Table("Users")]
    public class User
    {
        public long Id { get; set; }

        [Required] [MaxLength(20)] public string FirstName { get; set; }

        [Required] [MaxLength(20)] public string LastName { get; set; }

        [MaxLength(20)] public string MiddleName { get; set; }

        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }

        public string Fio => string.Format("{0} {1} {2}", LastName, FirstName, MiddleName);

        public string ShortFio
        {
            get
            {
                var fio = string.Format("{0} {1}.", LastName, FirstName?.First());
                if (!string.IsNullOrWhiteSpace(MiddleName))
                    fio += string.Format(" {0}.", MiddleName.First());
                return fio;
            }
        }

        public Student Student { get; set; }

        public Teacher Teacher { get; set; }
    }
}