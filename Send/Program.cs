using System;
using System.Text;
using RabbitMQ.Client;
using System.Threading;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            ThreadStart start = new ThreadStart(RunTask);
            Thread thread1 = new Thread(start);
            Thread thread2 = new Thread(start);
            Thread thread3 = new Thread(start);

            thread1.Start();
            thread2.Start();
            thread3.Start();
        }

        static void RunTask()
        {
            for (int i = 0; i < 300000; i++)
            {
                SendMessage(i);
            }
        }
        static void SendMessage(int i)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string message = string.Format("Mensagem {0}",i);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
