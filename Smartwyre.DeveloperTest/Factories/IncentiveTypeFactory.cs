using Smartwyre.DeveloperTest.Services.RebateCalculators;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Factories
{
    public class IncentiveTypeFactory : IIncentiveTypeFactory
    {
        public IRebateCalculator Create(IncentiveType incentiveType)
        {
            switch (incentiveType)
            {
                case IncentiveType.FixedRateRebate:
                    return new FixedRateRebateCalculator();
                case IncentiveType.FixedCashAmount:
                    return new FixedCashAmountRebateCalculator();
                case IncentiveType.AmountPerUom:
                    return new AmountPerUomRebateCalculator();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
