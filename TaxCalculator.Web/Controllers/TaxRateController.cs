using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using TaxCalculator.Domain;

namespace TaxCalculator.Web.Controllers
{
    public class TaxRateController : BaseController
    {
        public ActionResult Index()
        {
            var list = TaxCalculatorContext.TaxRates
                .OrderByDescending(r => r.Year)
                .ToArray();

            return View(list);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var taxRate = TaxCalculatorContext.TaxRates.Find(id);

            if (taxRate == null)
                return new HttpNotFoundResult();

            return View(taxRate);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var taxRate = new TaxRate
            {
                Items = new List<TaxRateItem>
                {
                    new TaxRateItem()
                    {
                        Thresholds = new List<TaxThreshold>
                        {
                            new TaxThreshold(),
                            new TaxThreshold(),
                            new TaxThreshold(),
                        }
                    }
                }
            };

            return View(taxRate);
        }

        [HttpPost]
        public ActionResult Add(TaxRate taxRate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TaxCalculatorContext.TaxRates.Add(taxRate);
                    taxRate.CreateAt = DateTime.Now;

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }

            return View(taxRate);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var taxRate = TaxCalculatorContext.TaxRates.Find(id);

            if (taxRate == null)
                return new HttpNotFoundResult();

            return View(taxRate);
        }

        [HttpPost]
        public ActionResult Edit(TaxRate taxRate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TaxCalculatorContext.Entry(taxRate).State = EntityState.Modified;
                    ShowInfoMessage("Tax rate saved successfully");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }

            return View(taxRate);
        }

        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            var taxRate = TaxCalculatorContext.TaxRates.Find(id);

            if (taxRate == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            TaxCalculatorContext.TaxRates.Remove(taxRate);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}