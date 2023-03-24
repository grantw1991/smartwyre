using FluentAssertions;
using Xunit;

namespace Smartwyre.DeveloperTest.Services.RebateCalculators
{
    public class AmountPerUomRebateCalculatorTests
    {
        private AmountPerUomRebateCalculator _calculator;

        public AmountPerUomRebateCalculatorTests()
        {
            _calculator = new AmountPerUomRebateCalculator();
        }

        [Fact]
        public void Calculate_WhenProductIsNull_ReturnsUnsuccessfulRebateResult()
        {
            var (amount, result) = _calculator.Calculate(null, new Types.Rebate(), 0);

            amount.Should().Be(0);
            result.Success.Should().BeFalse();
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
        public void Calculate_WhenVolumeIs0_ReturnsUnsuccessfulRebateResult()
        {
            var product = new Types.Product { SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom };
            var (amount, result) = _calculator.Calculate(product, new Types.Rebate { Amount = 10 }, 0);

            amount.Should().Be(0);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenRebateAmountIs0_ReturnsUnsuccessfulRebateResult()
        {
            var product = new Types.Product { SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom };
            var (amount, result) = _calculator.Calculate(product, new Types.Rebate { Amount = 0 }, 10);

            amount.Should().Be(0);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenSuccessfulCriteriaIsMet_ExpectedSuccessRepsonseIsReturned()
        {
            var product = new Types.Product { SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom };
            var (amount, result) = _calculator.Calculate(product, new Types.Rebate { Amount = 5 }, 10);

            amount.Should().Be(50);
            result.Success.Should().BeTrue();
        }
    }
}
