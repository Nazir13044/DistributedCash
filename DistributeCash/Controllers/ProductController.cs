using DistributedCash.Data;
using DistributedCash.Helper;
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
        //var apps= _appSetings;

        //var redisConnection = ConnectionMultiplexer.Connect(apps.RedisUrl);
        //var existingValue = redisConnection.GetDatabase();

        var existingValue = CashConnection.Connection.GetDatabase();

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
            pro = JsonSerializer.Deserialize<List<Product>>(cachedData);
        }
        
        return Ok(pro);
    }
}
