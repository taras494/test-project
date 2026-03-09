using System.Text.Json;
using TestProjectMVC.Models;

namespace TestProjectMVC.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiService> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<ArticleViewModel>> GetAllArticlesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/articles");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ArticleViewModel>>(json, JsonOptions) ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all articles from API");
            return [];
        }
    }

    public async Task<ArticleViewModel?> GetArticleByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/articles/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ArticleViewModel>(json, JsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching article {Id} from API", id);
            return null;
        }
    }

    public async Task<List<ArticleViewModel>> GetArticlesByTagAsync(string tag)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/articles/bytag/{Uri.EscapeDataString(tag)}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ArticleViewModel>>(json, JsonOptions) ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching articles by tag {Tag} from API", tag);
            return [];
        }
    }

    public async Task<List<ArticleViewModel>> SearchArticlesByTitleAsync(string title)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/articles/search?title={Uri.EscapeDataString(title)}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ArticleViewModel>>(json, JsonOptions) ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching articles by title {Title} from API", title);
            return [];
        }
    }

    public async Task<List<ProductViewModel>> GetAllProductsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/products");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ProductViewModel>>(json, JsonOptions) ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all products from API");
            return [];
        }
    }

    public async Task<ProductViewModel?> GetProductByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/products/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductViewModel>(json, JsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching product {Id} from API", id);
            return null;
        }
    }
}
