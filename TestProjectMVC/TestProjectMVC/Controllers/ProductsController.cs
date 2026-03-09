using Microsoft.AspNetCore.Mvc;
using TestProjectMVC.Services;

namespace TestProjectMVC.Controllers;

public class ProductsController : Controller
{
    private readonly IApiService _apiService;

    public ProductsController(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _apiService.GetAllProductsAsync();
        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _apiService.GetProductByIdAsync(id);
        if (product is null)
            return NotFound();

        return View(product);
    }
}
