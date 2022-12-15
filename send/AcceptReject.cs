using System.Text;
using RabbitMQ.Client;

class AcceptReject
{
    public static void publish()
    {
        var factory = new ConnectionFactory() { HostName = "localHost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        //Fanout Method
        channel.ExchangeDeclare(exchange: "ARX", type: ExchangeType.Fanout);
        while (true)
        {
            var message = "ARX Method working fine";
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "ARX",
                                routingKey: "",
                                basicProperties: null,
                                body: body);
            Console.WriteLine($"Sent \"{message}\" message to consumer");
            Console.WriteLine("Press any key to send message..");
            Console.ReadKey();
        }

    }

}