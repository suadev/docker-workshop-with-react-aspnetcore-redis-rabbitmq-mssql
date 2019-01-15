using System.Text;
using RabbitMQ.Client;

namespace api
{
    public class RabbitMqClient
    {
        public static void Publish(string message)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" }; // local=> HostName = "localhost"
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "app_logs",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: "app_logs",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}