namespace DistributedCash.Helper;

public static class Helper
{
    public static DateTimeOffset CreateCacheOptions(double expirationInMinutes = 39)
    {
        return DateTime.Now.AddMinutes(expirationInMinutes);
    }
}
