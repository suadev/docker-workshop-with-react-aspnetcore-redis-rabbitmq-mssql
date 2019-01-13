using System;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using aspnet_core_docker_workshop;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
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
            RabbitMqClient.Publish($"{model.Key} is successfully inserted into Redis");
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
