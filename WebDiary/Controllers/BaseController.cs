using System.Web.Mvc;

namespace WebDiary.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["Id"] == null)
            {
                Response.Redirect("~/News/Index"); //ПРЕДОТВРАТИЛИ ДОСТУП НА ГЛАВНОЕ МЕНЮ, ЕСЛИ ПОЛЬЗОВАТЕЛЬ НЕ ЗАШЕЛ ЧЕРЕЗ ЛОГИН
            }

            //Данный пример лучше не копипастить в каждое представление, можно просто добавить эти строчки в файл Layout
        }
    }
}