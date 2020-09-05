using RediCom;
using RediCom.Redis;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            IRedisCache cache = new RedisCache();


            cache.Save(DB.DB01, "test-key", "this is a data", new System.TimeSpan(0,5,0));

            var data = cache.Get(DB.DB02, "test-key");
        }
    }
}
