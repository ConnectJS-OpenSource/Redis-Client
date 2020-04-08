using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RediCom.Redis
{
    public class RedisCache : IRedisCache
    {
        RedisSettings Settings { get; set; }
        RedisCacheClient Client { get; set; }

        const string TICKET_PDF_QUEUE = "ticket-pdfs";

        Node RedisNode { get; set; }
        IRedisCacheConnectionPoolManager connection;
        RedisConfiguration redisConfiguration;
        bool Enabled;


        public RedisCache()
        {
            this.Settings = new RedisSettings(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Redis-Config.json"));
            this.RedisNode = this.Settings.Server.Redis;
            this.Enabled = this.Settings.Server.Enabled;
            if (Enabled) this.InitRedis();
        }

        private void InitRedis()
        {

            redisConfiguration = new RedisConfiguration()
            {
                AbortOnConnectFail = false,
                AllowAdmin = true,
                ConnectTimeout = this.Settings.Server.Timeout,
                Database = 0,
                Ssl = false,
                Password = this.RedisNode.Password,
                ServerEnumerationStrategy = new ServerEnumerationStrategy()
                {
                    Mode = ServerEnumerationStrategy.ModeOptions.All,
                    TargetRole = ServerEnumerationStrategy.TargetRoleOptions.Any,
                    UnreachableServerAction = ServerEnumerationStrategy.UnreachableServerActionOptions.IgnoreIfOtherAvailable
                },
            };

            var clusters = this.RedisNode.Clusters.ToList();
            redisConfiguration.Hosts = clusters
                .Select(m => new RedisHost { Host = m.Host, Port = m.Port })
                .ToArray();

            connection = new RedisCacheConnectionPoolManager(redisConfiguration);
            this.Client = new Lazy<RedisCacheClient>(() => new RedisCacheClient(connection, new NewtonsoftSerializer(), redisConfiguration), true).Value;

        }

        public bool Save(DB db, string key, object value, TimeSpan? expiry = null)
        {
            try
            {
                if (!Enabled) throw new Exception("Redis Service not enabled");
                return Task.Run<bool>(async () => await this.Client.GetDb((int)db).AddAsync(key, value, expiry ?? TimeSpan.MaxValue)).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> SaveAsync(DB db, string key, object value, TimeSpan? expiry)
        {
            try
            {
                if (!Enabled) throw new Exception("Redis Service not enabled");
                return await this.Client.GetDb((int)db).AddAsync(key, value, expiry ?? TimeSpan.MaxValue);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public object Get(DB db, string key)
        {
            try
            {
                if (!Enabled) throw new Exception("Redis Service not enabled");
                return Task.Run<object>(async () => await (this.Client.GetDb((int)db).GetAsync<object>(key))).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<object> GetAsync(DB db, string key)
        {
            try
            {
                if (!Enabled) throw new Exception("Redis Service not enabled");
                return await (this.Client.GetDb((int)db).GetAsync<object>(key));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Delete(DB db, string key)
        {
            try
            {
                if (!Enabled) throw new Exception("Redis Service not enabled");
                return Task.Run<bool>(async () => await (this.Client.GetDb((int)db).RemoveAsync(key))).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(DB db, string key)
        {
            try
            {
                if (!Enabled) throw new Exception("Redis Service not enabled");
                return await (this.Client.GetDb((int)db).RemoveAsync(key));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Clear(DB db)
        {
            try
            {
                if (!Enabled) throw new Exception("Redis Service not enabled");
                Task.Run(async () => await (this.Client.GetDb((int)db).FlushDbAsync())).Wait();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> ClearAsync(DB db)
        {
            try
            {
                if (!Enabled) throw new Exception("Redis Service not enabled");
                await (this.Client.GetDb((int)db).FlushDbAsync());
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public T GetItem<T>(DB db, string key, T Default = default)
        {
            try
            {
                if (!Enabled) throw new Exception("Redis Service not enabled");
                return Task.Run<T>(async () => await (this.Client.GetDb((int)db).GetAsync<T>(key) ?? Task.FromResult(Default))).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> GetItemAsync<T>(DB db, string key, T Default = default)
        {
            try
            {
                if (!Enabled) throw new Exception("Redis Service not enabled");
                return await (this.Client.GetDb((int)db).GetAsync<T>(key) ?? Task.FromResult(Default));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
