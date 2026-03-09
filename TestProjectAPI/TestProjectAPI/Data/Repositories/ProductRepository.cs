using Microsoft.EntityFrameworkCore;
using TestProjectAPI.Models;

namespace TestProjectAPI.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(int id) =>
        await _context.Products.FindAsync(id);

    public async Task AddRangeAsync(IEnumerable<Product> products)
    {
        _context.Products.AddRange(products);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AnyAsync() =>
        await _context.Products.AnyAsync();
}
