using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateCalculators
{
    public interface IRebateCalculator
    {
        public (decimal, CalculateRebateResult) Calculate(Product product, Rebate rebate, decimal volume);
    }
}
