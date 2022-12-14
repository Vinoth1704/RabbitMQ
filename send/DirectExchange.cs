using System.Text;
using RabbitMQ.Client;

class DirectExchange
{
    public static void publish()
    {
        var factory = new ConnectionFactory() { HostName = "localHost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        //Direct Method
        channel.ExchangeDeclare(exchange: "Direct", type: ExchangeType.Direct);
        var message = "Direct Method working fine";
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "Direct",
                            routingKey: "First",
                            basicProperties: null,
                            body: body);
        Console.WriteLine($"Sent \"{message}\" message to consumer");


    }

}