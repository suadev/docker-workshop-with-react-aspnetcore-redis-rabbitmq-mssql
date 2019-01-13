using System.IO;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace api
{
    public class RabbitListener
    {
        ConnectionFactory factory { get; set; }
        IConnection connection { get; set; }
        IModel channel { get; set; }

        public void Register()
        {
            var logFilePath = "./logs.txt";
            channel.QueueDeclare(queue: "app_logs", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                File.AppendAllText(logFilePath, message + "\n");
            };
            channel.BasicConsume(queue: "app_logs", autoAck: true, consumer: consumer);
        }

        public void Deregister()
        {
            this.connection.Close();
        }

        public RabbitListener()
        {
            this.factory = new ConnectionFactory() { HostName = "rabbitmq" };
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
        }
    }
}