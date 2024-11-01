using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace web_app_performance
{
    public class ConsumidorMensageira
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            const string fila = "produto_cadastrado";
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(fila,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var mensagem = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Estoque atualizado: {mensagem}");
                };

                channel.BasicConsume(fila,
                                     autoAck: true, 
                                     consumer: consumer);

                Console.WriteLine("Aguardando mensagens...");
                Console.ReadLine();
            }
        }
    }
}

    

