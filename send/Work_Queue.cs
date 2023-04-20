using System.Text;
using RabbitMQ.Client;

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
            var random = new Random();
            int messageId = 1;
            while (true)
            {
                var publishingTime = random.Next(1, 4);
                string message = $"MessageId:{messageId}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($" Sending message Id:{messageId}");
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                messageId++;
            }
        }
    }

}