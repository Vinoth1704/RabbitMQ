using System.Text;
using RabbitMQ.Client;

class AlternateExchange
{
    public static void publish()
    {
        var factory = new ConnectionFactory() { HostName = "localHost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "DLX", type: ExchangeType.Fanout);
        //Direct Method
        channel.ExchangeDeclare(exchange: "MainExchange1", ExchangeType.Direct, arguments: new Dictionary<string, object> {
                                                                        { "alternate-exchange", "DLX" } });
        var message = " Method working fine";
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "MainExchange1",
                            routingKey: "Firt",
                            basicProperties: null,
                            body: body);
        Console.WriteLine($"Sent \"{message}\" message to consumer");


    }

}