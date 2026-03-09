using Microsoft.EntityFrameworkCore;
using TestProjectAPI.Models;

namespace TestProjectAPI.Data.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _context;

    public ArticleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Article>> GetAllAsync() =>
        await _context.Articles.ToListAsync();

    public async Task<Article?> GetByIdAsync(int id) =>
        await _context.Articles.FindAsync(id);

    public async Task<IEnumerable<Article>> GetByTagAsync(string tag) =>
        await _context.Articles.Where(a => a.Tag == tag).ToListAsync();

    public async Task<IEnumerable<Article>> SearchByTitleAsync(string title) =>
        await _context.Articles
            .Where(a => a.Title.ToLower().Contains(title.ToLower()))
            .ToListAsync();

    public async Task<Article> AddAsync(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task UpdateAsync(Article article)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Article article)
    {
        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
    }
}
