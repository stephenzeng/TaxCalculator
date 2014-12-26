using System.Web.Mvc;

namespace TaxCalculator.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}