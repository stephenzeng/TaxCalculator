using System.Data.Entity;
using TaxCalculator.Domain;

namespace TaxCalculator.Dal
{
    public interface ITaxCalculatorContext
    {
        DbSet<TaxRate> TaxRates { get; set; }
        DbSet<TaxRateItem> TaxRateItems { get; set; }
        DbSet<TaxThreshold> TaxThresholds { get; set; }
    }
}