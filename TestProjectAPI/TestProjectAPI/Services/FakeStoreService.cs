using System.Text.Json;
using System.Text.Json.Serialization;
using TestProjectAPI.Models;

namespace TestProjectAPI.Services;

public class FakeStoreService : IFakeStoreService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FakeStoreService> _logger;

    public FakeStoreService(HttpClient httpClient, ILogger<FakeStoreService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://fakestoreapi.com/products");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var rawProducts = JsonSerializer.Deserialize<List<FakeStoreProduct>>(json, options);

            if (rawProducts is null)
                return [];

            return rawProducts.Select(p => new Product
            {
                Id = p.Id,
                Title = p.Title ?? string.Empty,
                Price = (decimal)p.Price,
                Description = p.Description ?? string.Empty,
                Category = p.Category ?? string.Empty,
                Image = p.Image ?? string.Empty,
                Rating = new Rating
                {
                    Rate = p.Rating?.Rate ?? 0,
                    Count = p.Rating?.Count ?? 0
                }
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching products from FakeStore API");
            return [];
        }
    }

    private sealed class FakeStoreProduct
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("rating")]
        public FakeStoreRating? Rating { get; set; }
    }

    private sealed class FakeStoreRating
    {
        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
