using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("task_queue", true, false, false, null);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    
                    while (true)
                    {
                        var message = Console.ReadLine();//GetMessage(args);
                        var body = Encoding.UTF8.GetBytes(message);

                       
                        channel.BasicPublish(exchange: "",
                                             routingKey: "task_queue",
                                             basicProperties: properties,
                                             body: body);
                    }
                  
                }
            }
        }
        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
