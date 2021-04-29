using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Receive.Data;
using Receive.Domain;
using System;
using System.Text;

namespace Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MensagemContext db = new MensagemContext())
            {
                db.Database.EnsureCreated();
            }

            Receive();
        }

        static void Receive()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    
                    //using (MensagemContext db = new MensagemContext())
                    //{
                        //Mensagem msg = new Mensagem(message);
                        //db.Add<Mensagem>(msg);
                        //db.SaveChanges();
                    //}

                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);


           
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
