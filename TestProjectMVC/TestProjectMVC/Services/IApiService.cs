using TestProjectMVC.Models;

namespace TestProjectMVC.Services;

public interface IApiService
{
    Task<List<ArticleViewModel>> GetAllArticlesAsync();
    Task<ArticleViewModel?> GetArticleByIdAsync(int id);
    Task<List<ArticleViewModel>> GetArticlesByTagAsync(string tag);
    Task<List<ArticleViewModel>> SearchArticlesByTitleAsync(string title);
    Task<List<ProductViewModel>> GetAllProductsAsync();
    Task<ProductViewModel?> GetProductByIdAsync(int id);
}
