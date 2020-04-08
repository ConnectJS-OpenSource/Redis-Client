using RediCom;
using RediCom.Redis;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            IRedisCache cache = new RedisCache();
            cache.Save(DB.DB01, "test-key", "this is a data");

            var data = cache.Get(DB.DB01, "test-key");
        }
    }
}
