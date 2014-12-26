using System.Linq;
using System.Web.Mvc;
using TaxCalculator.Service.Iterfaces;
using TaxCalculator.Web.Models;

namespace TaxCalculator.Web.Controllers
{
    public class CalculatorController : BaseController
    {
        private readonly ICalculateService _calculateService;

        public CalculatorController(ICalculateService calculateService)
        {
            _calculateService = calculateService;
        }

        public ActionResult Index()
        {
            var list = TaxCalculatorContext.TaxRates
                .OrderByDescending(r => r.Year)
                .ToArray()
                .Select(r => new SelectListItem
                {
                    Text = r.Description,
                    Value = r.Id.ToString(),
                });

            ViewBag.TaxRatesList = list;

            return View();
        }

        [HttpPost]
        public ActionResult Index(TaxCalculateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var taxRate = TaxCalculatorContext.TaxRates.Find(model.SelectedYear);
                if (taxRate == null)
                    return HttpNotFound();

                model.Result = _calculateService.Calculate(taxRate, model.TaxableIncome.Value);
            }
            else
            {
                ShowErrorMessage("Please input valid data");
            }

            var list = TaxCalculatorContext.TaxRates
                .OrderByDescending(r => r.Year)
                .ToArray()
                .Select(r => new SelectListItem
                {
                    Text = r.Description,
                    Value = r.Id.ToString(),
                });

            ViewBag.TaxRatesList = list;

            return View(model);
        }
    }
}