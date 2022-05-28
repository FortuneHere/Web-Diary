using System.Web.Mvc;
using WebDiary.DB;

namespace WebDiary.Controllers
{

    public class HomeController : Controller
    {

        public HomeController()
        {
        }


        public ActionResult Contacts()
        {
            return View();
        }
    }
}