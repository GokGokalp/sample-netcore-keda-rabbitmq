using System;
using System.Text;
using RabbitMQ.Client;

namespace Send
{
    class Send
    {
        static void Main(string[] args)
        {
            string host = args[0];
            int messageCount = int.Parse(args[1]);

            var factory = new ConnectionFactory() { Uri = new Uri(host) };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                for (int i = 0; i < messageCount; i++)
                {
                    string message = $"Hello World: {i}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}