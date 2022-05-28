using System.Data.Entity;
using System.Linq;

namespace WebDiary.DB
{
    public class LoginRepository
    {
        private readonly ApplicationDbContext db;

        public LoginRepository() =>
            db = new ApplicationDbContext();

        public LoginResult Login(string login, string password)
        {
            var user = db.Users
                         .Include(u => u.Student)
                         .Include(u => u.Teacher)
                         .FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null)
            {
                return null;
            }

            return new LoginResult
            {
                Id = user.Id,
                Login = user.Login,
                Fio = user.Fio,
                ShortFio = user.ShortFio,
                UserType = user.Student != null
                    ? UserType.Student
                    : user.Teacher == null
                        ? UserType.Student
                        : user.Teacher.IsAdministrator
                            ? UserType.Admin
                            : UserType.Teacher
            };
        }
    }
}