using RediCom;
using RediCom.Redis;
using System;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            IRedisCache cache = new RedisCache();
            cache.Save(DB.DB01, "test-key", "this is a data");
            var data = cache.Get(DB.DB01, "test-key");
            Console.WriteLine(data.ToString());
            Console.WriteLine("Hello World!");
        }
    }
}
