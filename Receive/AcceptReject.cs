
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

class AcceptReject
{
    public static void publish()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare("ARX", ExchangeType.Fanout);

        channel.QueueDeclare("ARXQueue");
        channel.QueueBind("ARXQueue", "ARX", "");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            if (ea.DeliveryTag == 5)
            {
                channel.BasicAck(ea.DeliveryTag, multiple: true);
            }
            Console.WriteLine($"Received message : {message}");
        };

        channel.BasicConsume("ARXQueue", false, consumer);

        Console.WriteLine("Consuming");

    }
}