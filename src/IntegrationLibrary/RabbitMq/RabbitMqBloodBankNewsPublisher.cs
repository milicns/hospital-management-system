using System.Collections.Generic;
using System.Text;
using IntegrationLibrary.BloodBank.Dto;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace IntegrationLibrary.RabbitMq;

public static class RabbitMqBloodBankNewsPublisher
{
    public static void Send(List<CreateNewsDto> message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "bloodBankNewsQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish(exchange: "",
                routingKey: "bloodBankNewsQueue",
                basicProperties: null,
                body: body);

        }
    }
}