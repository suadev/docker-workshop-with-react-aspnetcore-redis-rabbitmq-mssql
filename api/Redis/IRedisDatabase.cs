using System.Collections.Generic;
using System.Threading.Tasks;

namespace api
{
    public interface IRedisDatabase
    {
        Task StringSetAsync(string key, string value);
        Task<string> StringGetAsync(string key);
        Task<Dictionary<string, string>> StringGetAllAsync(string pattern = "*");
    }
}