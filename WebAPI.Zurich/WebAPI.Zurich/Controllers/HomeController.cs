using System.Web.Mvc;

namespace WebAPI.Zurich.Controllers
{
    public class HomeController : Controller
    {
        public RedirectResult Index()
        {
            return Redirect("~/swagger/ui/index");
        }



        ////public ActionResult Index()
        ////{
        ////    return View();
        ////}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}