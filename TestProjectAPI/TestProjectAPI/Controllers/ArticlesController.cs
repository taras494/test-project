using Microsoft.AspNetCore.Mvc;
using TestProjectAPI.Models;
using TestProjectAPI.Services;

namespace TestProjectAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetAll() =>
        Ok(await _articleService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Article>> GetById(int id)
    {
        var article = await _articleService.GetByIdAsync(id);
        return article is null ? NotFound() : Ok(article);
    }

    [HttpGet("bytag/{tag}")]
    public async Task<ActionResult<IEnumerable<Article>>> GetByTag(string tag) =>
        Ok(await _articleService.GetByTagAsync(tag));

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Article>>> Search([FromQuery] string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return BadRequest("Title query parameter is required.");

        return Ok(await _articleService.SearchByTitleAsync(title));
    }

    [HttpPost]
    public async Task<ActionResult<Article>> Create([FromBody] Article article)
    {
        var created = await _articleService.CreateAsync(article);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Article article)
    {
        if (id != article.Id)
            return BadRequest("Id in route does not match Id in body.");

        var updated = await _articleService.UpdateAsync(id, article);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _articleService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
