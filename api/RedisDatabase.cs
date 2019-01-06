
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace aspnet_core_docker_workshop
{
    public class RedisDatabase : IRedisDatabase
    {
        private readonly IDatabase _db;
        ConnectionMultiplexer _connection;

        public RedisDatabase()
        {
            _connection = ConnectionMultiplexer.Connect("redis"); //local -> localhost:6379
            _db = _connection.GetDatabase();
        }

        public Task<Dictionary<string, string>> StringGetAllAsync(string pattern = "*")
        {
            return GetDataFromDataBase();
        }

        public async Task<string> StringGetAsync(string key)
        {
            return (string)(await _db.StringGetAsync(key));
        }

        public Task StringSetAsync(string key, string value)
        {
            return _db.StringSetAsync(key, value);
        }

        public async Task<Dictionary<string, string>> GetDataFromDataBase()
        {
            int dbName = 0;
            var dicKeyValue = new Dictionary<string, string>();
            var keys = _connection.GetServer("redis").Keys(dbName, pattern: "*");
            var keysArr = keys.Select(key => (string)key).ToArray();

            foreach (var key in keysArr)
            {
                dicKeyValue.Add(key, await GetFromDB(dbName, key));
            }
            return dicKeyValue;
        }

        public Task<RedisValue> GetFromDB(int dbName, string key)
        {
            var db = _connection.GetDatabase(dbName);
            return db.StringGetAsync(key);
        }
    }

    public interface IRedisDatabase
    {
        Task StringSetAsync(string key, string value);
        Task<string> StringGetAsync(string key);
        Task<Dictionary<string, string>> StringGetAllAsync(string pattern = "*");
    }
}