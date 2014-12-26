using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TaxCalculator.Domain;

namespace TaxCalculator.Dal
{
    public class TaxCalculatorContext : DbContext
    {
        public TaxCalculatorContext() : base("TaxCalculatorContext")
        {
        }

        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<TaxRateItem> TaxRateItems { get; set; }
        public DbSet<TaxThreshold> TaxThresholds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
