using StackExchange.Redis;

namespace proj_csharp_kiminoyume.Helpers
{
    public class RedisCacheHelper
    {
        public bool IsRedisServerDown()
        {
            try
            {
                var redis = ConnectionMultiplexer.Connect("RedisCacheUrl");
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
