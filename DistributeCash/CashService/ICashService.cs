namespace DistributedCash.CashService;

public interface ICashService
{
    bool TryGetValue<T>(string key, out T value);
    bool Set<T>(string key, T value, DateTimeOffset options);
    void Clear(string pattern);
}
//public class CahsService : ICashService
//{ 


//}