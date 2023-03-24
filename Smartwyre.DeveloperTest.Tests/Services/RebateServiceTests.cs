using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.RebateCalculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services
{
    public class RebateServiceTests
    {
        private readonly Mock<IIncentiveTypeFactory> _factory;
        private readonly Mock<IRebateDataStore> _rebateDataStore;
        private readonly Mock<IProductDataStore> _productDataStore;
        private readonly RebateService _rebateService;

        public RebateServiceTests()
        {
            _factory = new Mock<IIncentiveTypeFactory>();
            _rebateDataStore = new Mock<IRebateDataStore>();
            _productDataStore = new Mock<IProductDataStore>();
            _rebateService = new RebateService(_factory.Object, _rebateDataStore.Object, _productDataStore.Object);
        }

        [Fact]
        public void Calculate_GivenSuccessfulRebateCalculationResult_ReturnsExpectedResultAndCallsStoreCalculationResult()
        {
            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "helloWorld",
                ProductIdentifier = "goobyeUniverse", 
                Volume = 120
            };

            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom
            };
            var product = new Product();

            _rebateDataStore.Setup(x => x.GetRebate("helloWorld")).Returns(rebate);
            _productDataStore.Setup(x => x.GetProduct("goobyeUniverse")).Returns(product);

            var rebateCalculator = new Mock<IRebateCalculator>();
            _factory.Setup(x => x.Create(IncentiveType.AmountPerUom)).Returns(rebateCalculator.Object);

            rebateCalculator.Setup(x => x.Calculate(product, rebate, 120)).Returns((7, new CalculateRebateResult { Success = true }));

            var result = _rebateService.Calculate(request);

            _rebateDataStore.Verify(x => x.StoreCalculationResult(rebate, 7), Times.Once);
        }

        [Fact]
        public void Calculate_GivenUnsuccessfulRebateCalculationResult_ReturnsExpectedResultAndDoesNotCallCallsStoreCalculationResult()
        {
            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "helloWorld",
                ProductIdentifier = "goobyeUniverse",
                Volume = 120
            };

            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom
            };
            var product = new Product();

            _rebateDataStore.Setup(x => x.GetRebate("helloWorld")).Returns(rebate);
            _productDataStore.Setup(x => x.GetProduct("goobyeUniverse")).Returns(product);

            var rebateCalculator = new Mock<IRebateCalculator>();
            _factory.Setup(x => x.Create(IncentiveType.AmountPerUom)).Returns(rebateCalculator.Object);

            rebateCalculator.Setup(x => x.Calculate(product, rebate, 120)).Returns((7, new CalculateRebateResult { Success = false }));

            var result = _rebateService.Calculate(request);

            _rebateDataStore.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Never);
        }
    }
}
