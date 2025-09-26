using OrderAggregator.Abstractions;
using OrderAggregator.Models;

namespace OrderAggregator.Services;

public class AggregatorService
{
    private readonly IOrderRepository _orders;
    private readonly ICurrencyConverter _currency;
    private readonly ITaxCalculator _tax;

    public AggregatorService(IOrderRepository orders, ICurrencyConverter currency, ITaxCalculator tax)
    {
        _orders = orders;
        _currency = currency;
        _tax = tax;
    }

    public (decimal totalRevenueUsd, List<CustomerRevenue> topCustomers, decimal avgOrderValue30d, List<TaxRegionSummary> taxByRegion) BuildReport()
    {
        var all = _orders.GetOrders();

        var revenuePerCustomer = new Dictionary<string, decimal>();
        decimal totlRevenue = 0m;
        var taxByRegion = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

        var cutoff = DateTime.UtcNow.AddDays(30);
        var recentOrders = new List<Order>();

        foreach(var o in all)
        {
            var orderAmountUsd = 0m;
            foreach(var li in o.Items)
            {
                var line = _currency.ConvertToUsd(li.UnitPrice * li.Quantity);
                var taxRate = _tax.GetRegionTaxRate(o.Region);
                var lineTax = line * taxRate;
                orderAmountUsd += line + lineTax;

                if(!taxByRegion.ContainsKey(o.Region))
                    taxByRegion[o.Region] = 0m;
                taxByRegion[o.Region] += lineTax;
            }

            if(!revenuePerCustomer.ContainsKey(o.CustomerId))
                revenuePerCustomer[o.CustomerId] = 0m;
            revenuePerCustomer[o.CustomerId] += orderAmountUsd;
            totlRevenue += orderAmountUsd;

            if(o.CreatedUtc >= cutoff)
                recentOrders.Add(o);
        }

        var topCustomers = revenuePerCustomer
            .OrderByDescending(kv => kv.Value)
            .ThenBy(kv => kv.Key)
            .Take(3)
            .Select(kv => new CustomerRevenue(kv.Key, kv.Value))
            .ToList();

        decimal avgRecent = 0m;
        if(recentOrders.Count > 0)
        {
            var totalRecent = recentOrders.Sum(o => o.Items.Sum(li => _currency.ConvertToUsd(li.UnitPrice * li.Quantity, o.Currency))); 
            avgRecent = totalRecent / recentOrders.Count;
        }

        var taxSummaries = taxByRegion.Select(kv => new TaxRegionSummary{ Region = kv.Key, TaxAmount = kv.Value }).ToList();

    return (totalRevenue, topCustomers, avgRecent, taxSummaries);
    }
}
