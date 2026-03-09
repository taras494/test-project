using TestProjectAPI.Models;

namespace TestProjectAPI.Services;

public interface IArticleService
{
    Task<IEnumerable<Article>> GetAllAsync();
    Task<Article?> GetByIdAsync(int id);
    Task<IEnumerable<Article>> GetByTagAsync(string tag);
    Task<IEnumerable<Article>> SearchByTitleAsync(string title);
    Task<Article> CreateAsync(Article article);
    Task<bool> UpdateAsync(int id, Article article);
    Task<bool> DeleteAsync(int id);
}
