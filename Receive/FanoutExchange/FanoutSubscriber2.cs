using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class FanoutSubscriber2
{
    public static void Subscribe()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        //Fanout Method
        channel.ExchangeDeclare(exchange: "fanout", ExchangeType.Fanout);
        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName, exchange: "fanout", routingKey: "");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("2nd Consumer received " + message);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        Console.WriteLine("2nd consumer consuming...");

    }

}