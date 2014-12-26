using TaxCalculator.Domain;
using TaxCalculator.Service.Dto;

namespace TaxCalculator.Service.Iterfaces
{
    public interface ICalculateService
    {
        Result Calculate(TaxRate taxRate, decimal income);
    }
}