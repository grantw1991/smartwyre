using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.RebateCalculators;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IProductDataStore, ProductDataStore>()
            .AddSingleton<IRebateDataStore, RebateDataStore>()
            .AddSingleton<IIncentiveTypeFactory, IncentiveTypeFactory>()
            .AddSingleton<IRebateCalculator, FixedRateRebateCalculator>()
            .AddSingleton<IRebateCalculator, FixedCashAmountRebateCalculator>()
            .AddSingleton<IRebateCalculator, AmountPerUomRebateCalculator>()
            .AddSingleton<IRebateService, RebateService>()
            .BuildServiceProvider();
                
        var rebateService = serviceProvider.GetService<IRebateService>();

        var result = rebateService.Calculate(new Types.CalculateRebateRequest
        {
            ProductIdentifier = GetArgValueWithDefault(args, 0, "product"),
            RebateIdentifier = GetArgValueWithDefault(args, 1, "Rebate"),
            Volume = decimal.Parse(GetArgValueWithDefault(args, 2, "0"))
        });

        Console.WriteLine($"Rebase Result: {result.Success}");
    }

    private static string GetArgValueWithDefault(string[] args, int index, string defaultValue)
    {
        return args.Length - 1 >= index ? args[index] : defaultValue;
    }
}

