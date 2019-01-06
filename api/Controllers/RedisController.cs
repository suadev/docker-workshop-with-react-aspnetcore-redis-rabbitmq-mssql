using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Post([FromBody]RedisSetModel model)
        {
            await _redisDatabase.StringSetAsync($"{model.Key}", model.Value);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            return Ok(await _redisDatabase.StringGetAsync(id));
        }

        public async Task<ActionResult> GetAll()
        {
            var list = await _redisDatabase.StringGetAllAsync();
            return Ok(list.OrderBy(s => s.Key));
        }
    }

    public class RedisSetModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
