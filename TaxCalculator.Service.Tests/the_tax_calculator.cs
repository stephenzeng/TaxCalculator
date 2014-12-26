using System;
using System.Collections.Generic;
using NUnit.Framework;
using TaxCalculator.Domain;

namespace TaxCalculator.Service.Tests
{
    [TestFixture]
    public class the_tax_calculator
    {
        private readonly TaxRate _taxRate = new TaxRate
        {
            Year = 2014,
            Description = "Tax rates in finacial year 2014 - 2015",
            Items = new List<TaxRateItem>
            {
                new TaxRateItem
                {
                    Description = "Income tax rates",
                    Thresholds = new List<TaxThreshold>
                    {
                        new TaxThreshold {Start = 0m, End = 18200m, Rate = 0m},
                        new TaxThreshold {Start = 18200m, End = 37000m, Rate = 0.19m},
                        new TaxThreshold {Start = 37000m, End = 80000m, Rate = 0.325m},
                        new TaxThreshold {Start = 80000m, End = 180000m, Rate = 0.37m},
                        new TaxThreshold {Start = 180000m, End = null, Rate = 0.45m},
                    }
                },
                new TaxRateItem
                {
                    Description = "Medicare levy rates",
                    Thresholds = new List<TaxThreshold>()
                    {
                        new TaxThreshold {Start = 0m, Rate = 0m},
                        new TaxThreshold {Start = 20542m, Rate = 0.1m},
                        new TaxThreshold {Start = 24167m, Rate = 0.02m},
                    }
                }
            }
        };

        [Test]
        public void throw_exception_if_income_not_larger_than_0()
        {
            //arrange
            var service = new CalculateService();

            // assert
            Assert.Catch<ArgumentException>(() => service.Calculate(_taxRate, -0.1m));
            Assert.Catch<ArgumentException>(() => service.Calculate(_taxRate, 0m));
        }

        [Test]
        public void calculate_tax_19404()
        {
            //arrange
            var service = new CalculateService();

            // act
            var result = service.Calculate(_taxRate, 19404.004m);

            // assert
            Assert.IsNotEmpty(result.Description);
            Assert.AreEqual(19404.004m, result.TaxableIncome);
            Assert.AreEqual(228.76076m, result.TotalAmount);
        }

        [Test]
        public void calculate_tax_200000()
        {
            //arrange
            var service = new CalculateService();

            // act
            var result = service.Calculate(_taxRate, 200000m);

            // assert
            Assert.IsNotEmpty(result.Description);
            Assert.AreEqual(200000m, result.TaxableIncome);
            Assert.AreEqual(67426.16m, result.TotalAmount);
        }
    }
}
