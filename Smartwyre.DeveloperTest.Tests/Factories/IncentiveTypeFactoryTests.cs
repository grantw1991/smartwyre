using FluentAssertions;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Services.RebateCalculators;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Factories
{
    public class IncentiveTypeFactoryTests
    {
        [Theory]
        [InlineData(IncentiveType.FixedRateRebate, typeof(FixedRateRebateCalculator))]
        [InlineData(IncentiveType.FixedCashAmount, typeof(FixedCashAmountRebateCalculator))]
        [InlineData(IncentiveType.AmountPerUom, typeof(AmountPerUomRebateCalculator))]
        public void Create_WhenIncentive(IncentiveType incentiveType, Type convertedType)
        {
            var factory = new IncentiveTypeFactory();

            factory.Create(incentiveType).Should().BeOfType(convertedType);
        }
    }
}
