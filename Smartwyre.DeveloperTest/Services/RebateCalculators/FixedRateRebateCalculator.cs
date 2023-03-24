using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateCalculators
{
    public class FixedRateRebateCalculator : IRebateCalculator
    {
        public (decimal, CalculateRebateResult) Calculate(Product product, Rebate rebate, decimal volume)
        {
            var (rebateAmount, rebateResult) = (decimal.Zero, new CalculateRebateResult { Success = false });

            if (rebate == null || 
                product == null || 
                !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) || 
                (rebate.Percentage == 0 || product.Price == 0 || volume == 0))
            {
                return (rebateAmount, rebateResult);
            }

            rebateAmount += product.Price * rebate.Percentage * volume;
            rebateResult.Success = true;
            return (rebateAmount, rebateResult);
        }
    }
}
