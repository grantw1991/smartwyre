using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateCalculators
{
    public class FixedCashAmountRebateCalculator : IRebateCalculator
    {
        public (decimal, CalculateRebateResult) Calculate(Product product, Rebate rebate, decimal volume)
        {
            var (rebateAmount, rebateResult) = (decimal.Zero, new CalculateRebateResult { Success = false });

            if (rebate == null ||
                !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) ||
                rebate.Amount == 0)
            {
                return (rebateAmount, rebateResult);
            }

            rebateAmount = rebate.Amount;
            rebateResult.Success = true;
            return (rebateAmount, rebateResult);
        }
    }
}
