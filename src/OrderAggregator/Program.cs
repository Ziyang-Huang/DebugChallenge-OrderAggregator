using OrderAggregator.Data;
using OrderAggregator.Services;
using OrderAggregator.Abstractions;

var dataFile = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "data", "orders.json");
var repo = new FileOrderRepository(dataFile);
ICurrencyConverter currency = new CurrencyConverter();
ITaxCalculator tax = new TaxCalculator();
var aggregator = new AggregatorService(repo, currency, tax);

var report = aggregator.BuildReport();

Console.WriteLine($"Total Revenue (USD): {report.totalRevenueUsd:F2}");
Console.WriteLine("Top Customers:");
foreach(var c in report.topCustomers)
{
    Console.WriteLine($"  {c.CustomerId} {c.RevenueUsd:F2}");
}
Console.WriteLine($"Avg Order Value Last 30d (USD): {report.avgOrderValue30d:F2}");
Console.WriteLine("Tax By Region:");
foreach(var r in report.taxByRegion)
{
    Console.WriteLine($"  {r.Region}: {r.TaxAmount:F2}");
}
