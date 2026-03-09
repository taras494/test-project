using TestProjectAPI.Models;

namespace TestProjectAPI.Data.Repositories;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetAllAsync();
    Task<Article?> GetByIdAsync(int id);
    Task<IEnumerable<Article>> GetByTagAsync(string tag);
    Task<IEnumerable<Article>> SearchByTitleAsync(string title);
    Task<Article> AddAsync(Article article);
    Task UpdateAsync(Article article);
    Task DeleteAsync(Article article);
}
