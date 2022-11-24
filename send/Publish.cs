using System.Text;
using RabbitMQ.Client;

class Publish
{
    public static void publish()
    {
        var factory = new ConnectionFactory() { HostName = "localHost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "pub/sub", type: ExchangeType.Fanout);
        var message = "Hello world";

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "pub/sub",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
        Console.WriteLine("Sent {0}", message);

    }

}