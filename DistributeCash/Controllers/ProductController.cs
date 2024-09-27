using DistributedCash.Data;
using DistributedCash.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace DistributedCash.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IOptions<AppSetings> apsettings) : ControllerBase
{
    private readonly AppSetings _appSetings = apsettings.Value;

    [HttpGet]
    public IActionResult GetProducts()
    {
        ProductList productList = new ProductList();
        var sdfs= _appSetings;

        var configuration = ConfigurationOptions.Parse("127.0.0.1:6379");
        var redisConnection = ConnectionMultiplexer.Connect(configuration);
        var existingValue = redisConnection.GetDatabase();

        List<Product> pro = new List<Product>();

        var cacheKey = "unique-cache-key";
        
        string cachedData = existingValue.StringGet(cacheKey);
        if (cachedData == null)
        {
            pro = productList.products();
            existingValue.StringSet(cacheKey, JsonSerializer.Serialize(pro), TimeSpan.FromMinutes(10));

        }
        else
        {
            pro = JsonSerializer.Deserialize<List<Product>>((cachedData));
        }
        
        return Ok(pro);
    }
}
