using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class DirectSubscriber1
{
    public static void Subscribe()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        //Direct Method
        channel.ExchangeDeclare(exchange: "Direct", ExchangeType.Direct);
        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName, exchange: "Direct", routingKey: "First");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("1st Consumer received " + message);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        Console.WriteLine("1st consumer consuming...");
        Console.ReadKey();
    }

}