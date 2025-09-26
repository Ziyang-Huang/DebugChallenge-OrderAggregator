using OrderAggregator.Models;

namespace OrderAggregator.Abstractions;

public interface IOrderRepository
{
    IEnumerable<Order> GetOrders();
}

public interface ICurrencyConverter
{
    decimal ConvertToUsd(decimal amount, string fromCurrency);
}

public interface ITaxCalculator
{
    (decimal rate, bool found) GetRegionTaxRate(string region);
}
