using Microsoft.AspNetCore.Mvc;
using TestProjectMVC.Models;
using TestProjectMVC.Services;

namespace TestProjectMVC.Controllers;

public class ArticlesController : Controller
{
    private readonly IApiService _apiService;

    public ArticlesController(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var articles = await _apiService.GetAllArticlesAsync();
        return View(articles);
    }

    public async Task<IActionResult> ByTag(string tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
            return RedirectToAction(nameof(Index));

        var articles = await _apiService.GetArticlesByTagAsync(tag);
        ViewBag.Tag = tag;
        return View(articles);
    }

    public async Task<IActionResult> Search(string? title)
    {
        ViewBag.SearchTitle = title;

        if (string.IsNullOrWhiteSpace(title))
            return View(new List<ArticleViewModel>());

        var articles = await _apiService.SearchArticlesByTitleAsync(title);
        return View(articles);
    }

    public async Task<IActionResult> Details(int id)
    {
        var article = await _apiService.GetArticleByIdAsync(id);
        if (article is null)
            return NotFound();

        return View(article);
    }
}
