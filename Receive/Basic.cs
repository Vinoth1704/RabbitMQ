using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class Basic
{
    public static void basic()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        {
            channel.QueueDeclare(queue: "basic",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received {message}");
            };
            channel.BasicConsume(queue: "basic",
                                 autoAck: true,
                                 consumer: consumer);
            Console.WriteLine("Waiting for the message...");
            Console.ReadLine();
        }
    }

}