using System.Text;
using RabbitMQ.Client;

class FanoutExchange
{
    public static void publish()
    {
        var factory = new ConnectionFactory() { HostName = "localHost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        //Fanout Method
        channel.ExchangeDeclare(exchange: "fanout", type: ExchangeType.Fanout);
        var message = "Fanout Method working fine";
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "fanout",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
        Console.WriteLine($"Sent \"{message}\" message to consumer");

    }

}