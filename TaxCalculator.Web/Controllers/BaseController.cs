using System.Web.Mvc;
using TaxCalculator.Dal;

namespace TaxCalculator.Web.Controllers
{
    public class BaseController : Controller
    {
        public TaxCalculatorContext TaxCalculatorContext { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            TaxCalculatorContext = (TaxCalculatorContext)HttpContext.Items["CurrentRequestDataContext"];
        }

        protected void ShowInfoMessage(string message)
        {
            ViewBag.InfoMessage = message;
        }

        protected void ShowWarningMessage(string message)
        {
            ViewBag.WarningMessage = message;
        }

        protected void ShowErrorMessage(string message)
        {
            ViewBag.ErrorMessage = message;
        }
    }
}