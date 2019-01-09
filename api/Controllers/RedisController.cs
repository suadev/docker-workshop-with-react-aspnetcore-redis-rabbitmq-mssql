using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_core_docker_workshop.Controllers
{
    [Route("api/[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly IRedisDatabase _redisDatabase;
        public RedisController(IRedisDatabase redisDatabase)
        {
            _redisDatabase = redisDatabase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RedisCreateModel model)
        {
            await _redisDatabase.StringSetAsync($"{model.Key}", model.Value);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _redisDatabase.StringGetAllAsync();
            return Ok(list.OrderBy(s => s.Key));
        }
    }
}
