using TestProjectAPI.Models;

namespace TestProjectAPI.Services;

public interface IFakeStoreService
{
    Task<List<Product>> GetProductsAsync();
}
