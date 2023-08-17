using StackExchange.Redis;

namespace proj_csharp_kiminoyume.Helpers
{
    public static class RedisCacheHelper
    {
        public static bool IsRedisServerDown()
        {
            try
            {
                // @KAT TODO: Get from appsettings
                var redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                var server = redis.GetServer(redis.GetEndPoints().First());

                return !server.IsConnected;
            }
            catch
            {
                return true;
            }
        }
    }
}
