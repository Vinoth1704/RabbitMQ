using System.Text;
using RabbitMQ.Client;

class DeadLetterExchange
{
    public static void publish()
    {
        var factory = new ConnectionFactory() { HostName = "localHost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        //Direct Method
        channel.ExchangeDeclare(exchange: "MainExchange", type: ExchangeType.Direct);
        var message = " Method working fine";
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "MainExchange",
                            routingKey: "First",
                            basicProperties: null,
                            body: body);
        Console.WriteLine($"Sent \"{message}\" message to consumer");


    }

}