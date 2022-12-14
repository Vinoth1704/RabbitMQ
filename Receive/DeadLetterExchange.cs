using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class DeadLetterExchange
{
    public static void publish()
    {
        var factory = new ConnectionFactory() { HostName = "localHost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();


        //Direct Method
        channel.ExchangeDeclare(exchange: "MainExchange", ExchangeType.Direct);

        //Main Queue
        channel.QueueDeclare(queue: "MainExchangeQueue", arguments: new Dictionary<string, object> {
                                                                        { "x-dead-letter-exchange", "DLX" },
                                                                        { "x-message-ttl", 5000 } }); //If the message not received by the main exchange for 5 secs it will be sent to DLX exchange
        channel.QueueBind(queue: "MainExchangeQueue", exchange: "MainExchange", routingKey: "First");

        var MainConsumer = new EventingBasicConsumer(channel);
        MainConsumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Main Consumer received " + message);
        };

        // channel.BasicConsume(queue: "MainExchangeQueue", autoAck: true, consumer: MainConsumer);




        //Dead Letter Exchange
        channel.ExchangeDeclare(exchange: "DLX", type: ExchangeType.Fanout);

        //DLX Queue
        channel.QueueDeclare(queue: "DLXExchangeQueue");
        channel.QueueBind(queue: "DLXExchangeQueue", exchange: "DLX", routingKey: "");

        var DLXconsumer = new EventingBasicConsumer(channel);
        DLXconsumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("DLX Consumer received " + message);
        };
        channel.BasicConsume(queue: "DLXExchangeQueue", autoAck: true, consumer: DLXconsumer);

        Console.WriteLine("Main consumer consuming...");
        Console.ReadKey();
    }
}