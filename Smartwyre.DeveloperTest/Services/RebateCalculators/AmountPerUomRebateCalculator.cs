using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateCalculators
{
    public class AmountPerUomRebateCalculator : IRebateCalculator
    {
        public (decimal, CalculateRebateResult) Calculate(Product product, Rebate rebate, decimal volume)
        {
            var (rebateAmount, rebateResult) = (decimal.Zero, new CalculateRebateResult { Success = false });

            if (rebate == null || 
                product == null || 
                !product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) || 
                (rebate.Amount == 0 || volume == 0))
            {
                return (rebateAmount, rebateResult);
            }

            rebateAmount += rebate.Amount * volume;
            rebateResult.Success = true;
            return (rebateAmount, rebateResult);
        }
    }
}
