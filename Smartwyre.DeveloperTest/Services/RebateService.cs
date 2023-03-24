using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IIncentiveTypeFactory _factory;
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;

    public RebateService(IIncentiveTypeFactory factory, IRebateDataStore rebateDataStore, IProductDataStore productDataStore)
    {
        _factory = factory;
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = _productDataStore.GetProduct(request.ProductIdentifier);

        var rebateCalculator = _factory.Create(rebate.Incentive);

        var (rebateAmount, result) = rebateCalculator.Calculate(product, rebate, request.Volume);

        if (result.Success)
        {
            _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
        }

        return result;
    }
}
