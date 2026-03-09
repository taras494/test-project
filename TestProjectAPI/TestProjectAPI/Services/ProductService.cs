using TestProjectAPI.Data.Repositories;
using TestProjectAPI.Models;

namespace TestProjectAPI.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Product>> GetAllAsync() =>
        _repository.GetAllAsync();

    public Task<Product?> GetByIdAsync(int id) =>
        _repository.GetByIdAsync(id);
}
