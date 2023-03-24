using FluentAssertions;
using Xunit;

namespace Smartwyre.DeveloperTest.Services.RebateCalculators
{
    public class FixedCashAmountRebateCalculatorTests
    {
        private FixedCashAmountRebateCalculator _calculator;

        public FixedCashAmountRebateCalculatorTests()
        {
            _calculator = new FixedCashAmountRebateCalculator();
        }

        [Fact]
        public void Calculate_WhenRebateIsNull_ReturnsUnsuccessfulRebateResult()
        {
            var (amount, result) = _calculator.Calculate(new Types.Product(), null, 0);

            amount.Should().Be(0);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenSupportedIncentivesDoesNotHaveExpectedFlag_ReturnsUnsuccessfulRebateResult()
        {
            var product = new Types.Product { SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate };
            var (amount, result) = _calculator.Calculate(product, new Types.Rebate(), 0);

            amount.Should().Be(0);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenRebateAmountIs0_ReturnsUnsuccessfulRebateResult()
        {
            var product = new Types.Product { SupportedIncentives = Types.SupportedIncentiveType.FixedCashAmount };
            var (amount, result) = _calculator.Calculate(product, new Types.Rebate { Amount = 0 }, 0);

            amount.Should().Be(0);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenSuccessfulCriteriaIsMet_ExpectedSuccessRepsonseIsReturned()
        {
            var product = new Types.Product { SupportedIncentives = Types.SupportedIncentiveType.FixedCashAmount };
            var (amount, result) = _calculator.Calculate(product, new Types.Rebate { Amount = 5 }, 0);

            amount.Should().Be(5);
            result.Success.Should().BeTrue();
        }
    }
}
