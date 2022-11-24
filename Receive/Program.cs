using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

class Receive
{
    public static void Main()
    {
        // Basic.basic();
        // Work_Queue.workQueue();
        Subscribe.subscribe();
        Second_Subscriber.secondSubscriber();
        Console.ReadKey();
    }
}