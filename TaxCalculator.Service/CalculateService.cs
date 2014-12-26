using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaxCalculator.Domain;
using TaxCalculator.Service.Dto;
using TaxCalculator.Service.Iterfaces;

namespace TaxCalculator.Service
{
    public class CalculateService : ICalculateService
    {
        public Result Calculate(TaxRate taxRate, decimal income)
        {
            if (income <= 0)
                throw new ArgumentException("income must be larger than 0");

            var result = new Result
            {
                Description = string.Format("Tax calculate result for income {0} in year {1} - {2}",
                    income.ToString("C"),
                    taxRate.Year,
                    taxRate.Year + 1),
                TaxableIncome = income,
                Items = new Collection<ResultItem>()
            };

            foreach (var item in taxRate.Items)
            {
                var taxItem = new ResultItem
                {
                    Name = item.Description,
                    Amount = CalculateTaxItem(item.Thresholds, income)
                };

                result.Items.Add(taxItem);
            }

            return result;
        }

        private static decimal CalculateTaxItem(IEnumerable<TaxThreshold> thresholds, decimal input)
        {
            var output = 0m;
            var temp = input;

            foreach (var rate in thresholds.Where(r => r.Start <= input).OrderByDescending(r => r.Start))
            {
                output += (temp - rate.Start) * rate.Rate;
                temp = rate.Start;
            }

            return output;
        }
    }
}
