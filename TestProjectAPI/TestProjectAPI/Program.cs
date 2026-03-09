using Microsoft.EntityFrameworkCore;
using TestProjectAPI.Data;
using TestProjectAPI.Data.Repositories;
using TestProjectAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddHttpClient<IFakeStoreService, FakeStoreService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
    if (!await productRepository.AnyAsync())
    {
        var fakeStoreService = scope.ServiceProvider.GetRequiredService<IFakeStoreService>();
        var products = await fakeStoreService.GetProductsAsync();
        if (products.Count > 0)
        {
            foreach (var p in products) p.Id = 0;
            await productRepository.AddRangeAsync(products);
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
