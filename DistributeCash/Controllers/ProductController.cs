using DistributedCash.CashService;
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
public class ProductController(IOptions<AppSetings> apsettings, ICashService cashServices) : ControllerBase
{
    private readonly AppSetings _appSetings = apsettings.Value;
    private readonly ICashService _cashService = cashServices;
    private readonly DateTimeOffset _options = Helper.Helper.CreateCacheOptions();

    [HttpGet]
    public IActionResult GetProducts()
    {
        ProductList productList = new ProductList();

        List<Product> pro = new List<Product>();

        var cacheKey = "unique-cache-key";
        string cachedData = string.Empty;


        if (_cashService.TryGetValue(key: cacheKey, value : out cachedData) ==false)
        {
            pro = productList.products();
            _cashService.Set(cacheKey, JsonSerializer.Serialize(pro), options : _options);
        }
        pro = JsonSerializer.Deserialize<List<Product>>(cachedData);
        
        return Ok(pro);
    }
}
