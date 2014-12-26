namespace TaxCalculator.Domain
{
    public class TaxThreshold
    {
        public int Id { get; set; }
        public decimal Start { get; set; }
        public decimal? End { get; set; }
        public decimal Rate { get; set; }

        public bool IsActive { get; set; }
    }
}
