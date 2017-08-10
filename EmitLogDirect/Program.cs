using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmitLogDirect
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Direct Logs
                channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");
                Console.WriteLine(" Press Ctrl + C to exit.");
                while (true) //write type of routing key for receiver processed
                {
                    Console.WriteLine("Write Severity Name:");
                    var severity = Console.ReadLine();//GetMessage(args);
                    Console.WriteLine("Write Message Inf:");
                    var message = Console.ReadLine();
                    var body = Encoding.UTF8.GetBytes(message);
                    //Publish severals situations like INFO, WARNING, ERROR, ETC.
                    channel.BasicPublish(exchange: "direct_logs",
                                         routingKey: severity,
                                         basicProperties: null,
                                         body: body);
                    //Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
