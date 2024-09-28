using DistributedCash.Model;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DistributedCash.Helper;



internal class CashConnection
{
    private readonly AppSetings _appSettings;
    public static string RedisUrl;
    private static readonly Lazy<ConnectionMultiplexer> lazyConnection;
    static CashConnection()
    {
        if (string.IsNullOrEmpty(RedisUrl) == false)
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                {
                    return ConnectionMultiplexer.Connect("127.0.0.1:6379");
                });
        }
    }
    internal static ConnectionMultiplexer Connection => lazyConnection.Value;
}
