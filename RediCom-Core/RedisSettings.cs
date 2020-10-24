using Newtonsoft.Json;
using System.Collections.Generic;

namespace RediCom.Redis
{
    public class RedisSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public List<Cluster> Clusters { get; set; }

        public static RedisSettings ProcessSettingsFile(string content)
        {
            return JsonConvert.DeserializeObject<RedisSettings>(content);
        }
    }

    public class Cluster
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }




}
