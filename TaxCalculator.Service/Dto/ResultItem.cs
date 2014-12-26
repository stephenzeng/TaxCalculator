using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Service.Dto
{
    public class ResultItem
    {
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}