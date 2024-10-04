using DistributedCash.Model;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DistributedCash.Helper;

internal static class CacheConfiguration
{
    internal static string RedUrl { get; }
    static CacheConfiguration()
    {
        try
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            RedUrl = configuration.GetSection("AppSetings").Get<AppSetings>()?.RedisUrl;
        }
        catch (FileNotFoundException e)
        {
            throw new Exception("The configuration file 'appsettings.json' was not found.", e);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }
}
internal class CacheConnection
{
    private static readonly string redisUrl;
    private static readonly Lazy<ConnectionMultiplexer> lazyConnection;
    static CacheConnection()
    {
        try
        {
            redisUrl = CacheConfiguration.RedUrl;
            if (string.IsNullOrEmpty(redisUrl) == false)
            {
                lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                {
                    return ConnectionMultiplexer.Connect(redisUrl);
                });
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }
    internal static ConnectionMultiplexer Connection => lazyConnection?.Value;
    internal static bool RedisCacheEnbled => string.IsNullOrEmpty(redisUrl) == false;
}
