using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TaxCalculator.Service.Dto
{
    public class Result
    {
        public string Description { get; set; }
        public decimal TaxableIncome { get; set; }
        public ICollection<ResultItem> Items { get; set; }

        [Display(Name = "Total tax payable")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount
        {
            get { return Items.Sum(i => i.Amount); }
        }
    }
}