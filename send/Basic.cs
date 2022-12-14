using System.Text;
using RabbitMQ.Client;

class Basic
{
    public static void basic()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "basic",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        string message = "Basic Operation";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: "basic",
                             basicProperties: null,
                             body: body);
        Console.WriteLine($"Sent {message}");

        Console.ReadLine();
    }
}