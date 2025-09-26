namespace OrderAggregator.Models;

public class Order
{
    public string Id { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string Currency { get; set; } = "USD";
    public string Region { get; set; } = string.Empty; // e.g. US, EU, APAC
    public bool IsCancelled { get; set; }
    public bool IsRefunded { get; set; }
    public DateTime CreatedUtc { get; set; }
    public List<LineItem> Items { get; set; } = new();
}

public class LineItem
{
    public string Sku { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}

public record CustomerRevenue(string CustomerId, decimal RevenueUsd);

public class TaxRegionSummary
{
    public string Region { get; set; } = string.Empty;
    public decimal TaxAmount { get; set; }
}
