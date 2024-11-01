using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using web_app_domain;

namespace web_app_performance
{
    public class FilaProdutoCadastrado
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "produto_cadastrado",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                Produto produto = new Produto
                {
                    Id = 123,
                    Nome = "Teclado Gamer",
                    QuantidadeEstoque = 10
                };

                // Serializando o objeto Produto para JSON
                string mensagem = JsonConvert.SerializeObject(produto);
                var body = Encoding.UTF8.GetBytes(mensagem);

                channel.BasicPublish(exchange: "",
                                     routingKey: "produto_cadastrado",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($" {mensagem}");
            }
        }

    }
}
