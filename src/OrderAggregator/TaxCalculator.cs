using OrderAggregator.Abstractions;

namespace OrderAggregator.Services;

public class TaxCalculator : ITaxCalculator
{
    private static readonly Dictionary<string, decimal> _taxRates = new(StringComparer.OrdinalIgnoreCase)
    {
        {"US", 0.07m},
        {"EU", 0.19m},
        {"APAC", 0.10m}
    };

    public decimal GetRegionTaxRate(string region)
    {
        if(!_taxRates.TryGetValue(region, out var rate))
        {
            return 0.15m; // fallback arbitrary
        }
        return rate;
    }
}
