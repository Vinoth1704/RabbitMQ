using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class Work_Queue
{
    public static void workQueue()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);
            var random = new Random();
            consumer.Received += (model, ea) =>
            {
                var processingTime = random.Next(1, 6);
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message: {message} will take {processingTime} to process");
                Task.Delay(TimeSpan.FromSeconds(processingTime)).Wait();
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: "hello",
                                 autoAck: false,
                                 consumer: consumer);
            Console.WriteLine("Consuming..");
            Console.ReadLine();
        }

    }
}