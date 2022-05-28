using System.Web.Mvc;

namespace WebDiary.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult Index()
        {
            return View("../News/Index");
        }
    }
}