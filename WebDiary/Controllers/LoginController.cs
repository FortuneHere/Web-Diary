using System.Web.Mvc;
using WebDiary.DB;
using WebDiary.ViewModels;

namespace WebDiary.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository loginRepository;

        public LoginController()
        {
            loginRepository = new LoginRepository();
        }

        // GET: Login
        public ActionResult Index() =>
            View();

        [HttpPost]
        public ActionResult Authorize(LoginModel loginModel) //метод входа в аккаунт
        {
            //сравниваем логин и пароль между БД и Представлением
            var loginResult = loginRepository.Login(loginModel.Login, loginModel.Password);
            if (loginResult == null)
            {
                // ModelState.AddModelError("Login", "Не правильные логин или пароль");
                // return View("Index", loginModel);
                return Json(new {success = false, error = "Не правильные логин или пароль"});
            }

            Session["Id"] = loginResult.Id; //сохраняем Id во время Session
            Session["Fio"] = loginResult.Fio; //!!!МОЖНО ВЫВОДИТЬ ДАННЫЕ ЮЗЕРА ВО ВРЕМЯ ЕГО ПРИСУТСТВИЯ(ФИО)!!!
            Session["ShortFio"] = loginResult.ShortFio; //!!!МОЖНО ВЫВОДИТЬ ДАННЫЕ ЮЗЕРА ВО ВРЕМЯ ЕГО ПРИСУТСТВИЯ(ФИО)!!!
            Session["UserType"] = loginResult.UserType;

            if (loginResult.UserType == UserType.Student)
            {
                //return RedirectToAction("Index", "StudentSchedule");
                return Json(new {success = true});
            }

            //return RedirectToAction("Index", "Home"); //при успешном логине переходим в Home страницу
            return Json(new {success = true});
        }

        public ActionResult LogOut() //метод выхода из аккаунта
        {
            var Id = (long) Session["Id"]; //!!!выводит айди пользователя ему!!! Я его не использовал
            Session.Abandon();
            return RedirectToAction("Index", "News");
        }
    }
}