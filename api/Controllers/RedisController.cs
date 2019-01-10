using System;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_core_docker_workshop.Controllers
{
    [Route("api/[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly IRedisDatabase _redisDatabase;
        private readonly IBus _bus;
        public RedisController(IRedisDatabase redisDatabase, IBus bus)
        {
            _bus = bus;
            _redisDatabase = redisDatabase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RedisCreateModel model)
        {
            await _redisDatabase.StringSetAsync($"{model.Key}", model.Value);
            var logEndpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost:5672/Logs"));

            await logEndpoint.Send<LogModel>(new
            {
                Time = DateTime.UtcNow,
                EventId = 1,
                Message = "Created redis key."
            });
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
