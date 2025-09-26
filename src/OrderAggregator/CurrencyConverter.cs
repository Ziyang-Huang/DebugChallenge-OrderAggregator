using OrderAggregator.Abstractions;

namespace OrderAggregator.Services;

public class CurrencyConverter : ICurrencyConverter
{
    // Fixed rates to USD
    private static readonly Dictionary<string, decimal> Rates = new(StringComparer.OrdinalIgnoreCase)
    {
        {"USD", 1m},
        {"EUR", 1.10m},
        {"CNY", 0.14m},
        {"JPY", 0.0068m},
    };

    public decimal ConvertToUsd(decimal amount, string fromCurrency)
    {
        if (!Rates.TryGetValue(fromCurrency, out var rate))
        {
            rate = 1m; // fallback: treat as already USD
        }

        return Math.Round(amount * rate, 2, MidpointRounding.ToZero);
    }
}
