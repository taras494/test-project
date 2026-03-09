using TestProjectAPI.Models;

namespace TestProjectAPI.Data.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddRangeAsync(IEnumerable<Product> products);
    Task<bool> AnyAsync();
}
