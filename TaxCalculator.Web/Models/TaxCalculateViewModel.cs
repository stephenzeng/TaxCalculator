using System.ComponentModel.DataAnnotations;
using TaxCalculator.Service.Dto;

namespace TaxCalculator.Web.Models
{
    public class TaxCalculateViewModel
    {
        [Required]
        [Display(Name = "Taxable income")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Must be a number without digits")]

        [DataType(DataType.Currency)]
        public int? TaxableIncome { get; set; }

        [Display(Name = "Financial year")]
        public int SelectedYear { get; set; }
        public Result Result { get; set; }
    }
}