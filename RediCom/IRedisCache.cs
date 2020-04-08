using System;
using System.Threading.Tasks;

namespace RediCom
{
    public enum DB
    {
        DB01,
        DB02,
        DB03,
        DB04,
        DB05,
        DB06,
        DB07,
        DB08,
        DB09,
        DB010,
        DB011,
        DB012,
        DB013,
        DB014,
        DB015,
        DB016
    }
    public interface IRedisCache
    {
        Task<bool> SaveAsync(DB db, string key, object value, TimeSpan? expiry);
        Task<object> GetAsync(DB db, string key);
        Task<bool> DeleteAsync(DB db, string key);
        Task<bool> ClearAsync(DB db);
        Task<T> GetItemAsync<T>(DB db, string key, T Default = default);

        bool Save(DB db, string key, object value, TimeSpan? expiry = null);
        object Get(DB db, string key);
        bool Delete(DB db, string key);
        bool Clear(DB db);
        T GetItem<T>(DB db, string key, T Default = default);
    }
}
