using TestProjectAPI.Data.Repositories;
using TestProjectAPI.Models;

namespace TestProjectAPI.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _repository;

    public ArticleService(IArticleRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Article>> GetAllAsync() =>
        _repository.GetAllAsync();

    public Task<Article?> GetByIdAsync(int id) =>
        _repository.GetByIdAsync(id);

    public Task<IEnumerable<Article>> GetByTagAsync(string tag) =>
        _repository.GetByTagAsync(tag);

    public Task<IEnumerable<Article>> SearchByTitleAsync(string title) =>
        _repository.SearchByTitleAsync(title);

    public async Task<Article> CreateAsync(Article article)
    {
        article.CreatedAt = DateTime.UtcNow;
        return await _repository.AddAsync(article);
    }

    public async Task<bool> UpdateAsync(int id, Article article)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            return false;

        existing.Title = article.Title;
        existing.Text = article.Text;
        existing.IsPublished = article.IsPublished;
        existing.Tag = article.Tag;
        existing.CreatedAt = article.CreatedAt;

        await _repository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var article = await _repository.GetByIdAsync(id);
        if (article is null)
            return false;

        await _repository.DeleteAsync(article);
        return true;
    }
}
