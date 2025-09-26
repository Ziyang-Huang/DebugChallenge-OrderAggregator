using System.Text.Json;
using OrderAggregator.Models;
using OrderAggregator.Abstractions;

namespace OrderAggregator.Data;

public class FileOrderRepository : IOrderRepository
{
    private readonly string _filePath;

    public FileOrderRepository(string filePath)
    {
        _filePath = filePath;
    }

    public List<Order> GetOrders()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Order>();
        }

        using stream = File.OpenRead(_filePath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var orders = JsonSerializer.Deserialize<List<Order>>(stream, options) ?? new List<Order>();
        return orders;
    }
}
