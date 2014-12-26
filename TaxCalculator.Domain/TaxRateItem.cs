using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Domain
{
    public class TaxRateItem
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<TaxThreshold> Thresholds { get; set; }
    }
}