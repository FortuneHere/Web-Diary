namespace WebDiary.DB
{
    public class LoginResult
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public UserType UserType { get; set; }
        public string Fio { get; set; }
        public string ShortFio { get; set; }
    }
}