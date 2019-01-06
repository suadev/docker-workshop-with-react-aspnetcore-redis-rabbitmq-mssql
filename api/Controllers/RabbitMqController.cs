using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace aspnet_core_docker_workshop.Controllers
{
    [Route("api/[controller]")]
    public class RabbitMqController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post([FromBody]RabbitMQSetModel model)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" }; //out of container => HostName = "localhost"
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: model.Queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(model.Message);
                channel.BasicPublish(exchange: "",
                                     routingKey: model.Queue,
                                     basicProperties: null,
                                     body: body);
            }
            return Ok($"The message: '{model.Message}' has been sent to the '{model.Queue}' queue.");
        }
    }

    public class RabbitMQSetModel
    {
        public string Queue { get; set; }
        public string Message { get; set; }
    }
}