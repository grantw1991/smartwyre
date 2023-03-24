using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Services.RebateCalculators;

namespace Smartwyre.DeveloperTest.Factories
{
    public interface IIncentiveTypeFactory
    {
        public IRebateCalculator Create(IncentiveType incentiveType);
    }
}
