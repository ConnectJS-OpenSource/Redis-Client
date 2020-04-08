using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;

namespace RediCom.Redis
{
    internal class RedisSettings
    {
        public string SettingsPath { get; set; }
        public Server Server { get; set; }
        public RedisSettings() { }
        public RedisSettings(string filePath)
        {
            this.SettingsPath = filePath;
            this.Init();
        }
        void Init()
        {
            var json = File.ReadAllText(SettingsPath);
            try
            {
                var obj = JsonConvert.DeserializeObject<RedisSettings>(json);
                this.Server = obj.Server;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    internal enum ConnectionMode
    {
        Prod,
        Dev
    }

    internal class Server
    {
        [JsonProperty("mode")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConnectionMode Mode { get; set; }

        public Node Dev { get; set; }
        public Node Prod { get; set; }
        public int Timeout { get; set; }
        public bool Enabled { get; set; }
        [JsonIgnore] public Node Redis => this.Mode == ConnectionMode.Prod ? this.Prod : this.Dev;
    }

    internal class Node
    {
        public string Password { get; set; }
        public List<Cluster> Clusters { get; set; }
    }

    internal class Cluster
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
