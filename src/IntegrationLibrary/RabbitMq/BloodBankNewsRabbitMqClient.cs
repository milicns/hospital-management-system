using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntegrationLibrary.BloodBank.Dto;
using IntegrationLibrary.BloodBank.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IntegrationLibrary.RabbitMq;

public class BloodBankNewsRabbitMqClient : BackgroundService
{
    IConnection connection;
    IModel channel;
    IServiceScopeFactory _serviceProvider;
    
    
    public BloodBankNewsRabbitMqClient(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceProvider = serviceScopeFactory;
    }
    
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest",Port = 5672};
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: "bloodBankNewsQueue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var jsonMessage = Encoding.UTF8.GetString(body);
            var deserializedNews = JsonConvert.DeserializeObject<List<CreateNewsDto>>(jsonMessage);
            SaveReceivedNews(deserializedNews);
            SaveDonationTerms(deserializedNews);

        };
       
        channel.BasicConsume(queue: "bloodBankNewsQueue",
            autoAck: true,
            consumer: consumer);
        return base.StartAsync(cancellationToken);
    }
    
    private void SaveReceivedNews(List<CreateNewsDto> receivedNews)
    {
        using var scope = _serviceProvider.CreateScope();
        
        foreach (var news in receivedNews)
        {
            scope.ServiceProvider.GetRequiredService<IBloodBankNewsService>().AddNews(news);
        }
        
    }

    private void SaveDonationTerms(List<CreateNewsDto> receivedNews)
    {
        using var scope = _serviceProvider.CreateScope();
        foreach (var news in receivedNews)
        {
            scope.ServiceProvider.GetRequiredService<IDonationTermsService>().AddTerms(news.Terms);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        channel.Close();
        connection.Close();
        return base.StopAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}