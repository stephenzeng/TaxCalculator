using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TaxCalculator.Domain;

namespace TaxCalculator.Dal
{
    public class TaxCalculatorInitializer : DropCreateDatabaseAlways<TaxCalculatorContext>
    {
        protected override void Seed(TaxCalculatorContext context)
        {
            AddTaxThresholds(context);
            AddTaxRateItems(context);
            AddTaxRates(context);
        }

        private static void AddTaxThresholds(TaxCalculatorContext context)
        {
            var taxThresholds = new List<TaxThreshold>()
            {
                new TaxThreshold {Id = 1, Start = 0m, End = 18200m, Rate = 0m},
                new TaxThreshold {Id = 2, Start = 18200m, End = 37000m, Rate = 0.19m},
                new TaxThreshold {Id = 3, Start = 37000m, End = 80000m, Rate = 0.325m},
                new TaxThreshold {Id = 4, Start = 80000m, End = 180000m, Rate = 0.37m},
                new TaxThreshold {Id = 5, Start = 180000m, End = null, Rate = 0.45m},
                new TaxThreshold {Id = 6, Start = 0m, Rate = 0m},
                new TaxThreshold {Id = 7, Start = 20542m, Rate = 0.1m},
                new TaxThreshold {Id = 8, Start = 24167m, Rate = 0.02m},
                new TaxThreshold {Id = 9, Start = 0m, End = 180000m, Rate = 0m},
                new TaxThreshold {Id = 10, Start = 180000m, Rate = 0.02m},
            };

            taxThresholds.ForEach(t => context.TaxThresholds.Add(t));
            context.SaveChanges();
        }

        private static void AddTaxRateItems(TaxCalculatorContext context)
        {
            var taxRateItems = new List<TaxRateItem>()
            {
                new TaxRateItem
                {
                    Id = 1,
                    Description = "Income tax rates",
                    Thresholds = new List<TaxThreshold>
                    {
                        context.TaxThresholds.Find(1),
                        context.TaxThresholds.Find(2),
                        context.TaxThresholds.Find(3),
                        context.TaxThresholds.Find(4),
                        context.TaxThresholds.Find(5),
                    }
                },
                new TaxRateItem
                {
                    Id = 2,
                    Description = "Medicare levy rates",
                    Thresholds = new List<TaxThreshold>()
                    {
                        context.TaxThresholds.Find(6),
                        context.TaxThresholds.Find(7),
                        context.TaxThresholds.Find(8),
                    },
                },
                new TaxRateItem
                {
                    Description = "Budget repair levy rates",
                    Thresholds = new List<TaxThreshold>
                    {
                        context.TaxThresholds.Find(9),
                        context.TaxThresholds.Find(10),
                    },
                },
            };

            taxRateItems.ForEach(i => context.TaxRateItems.Add(i));
            context.SaveChanges();
        }

        private static void AddTaxRates(TaxCalculatorContext context)
        {
            var taxRate = new TaxRate
            {
                Id = 1,
                Year = 2014,
                Description = "Tax rates in finacial year 2014 - 2015",
                CreateAt = DateTime.Now,
                Items = context.TaxRateItems.ToList(),
            };

            context.TaxRates.Add(taxRate);
            context.SaveChanges();
        }
    }
}