using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAppTests.Setup;
using IntegrationLibrary.BloodBank.Dto;
using IntegrationLibrary.BloodBank.Model;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace IntegrationAppTests.Integrations;

public class BloodBankNewsTests : BaseIntegrationTest
{
    public BloodBankNewsTests(TestDatabaseFactory<Startup> factory) : base(factory) {}

    [Fact]
    public async Task Get_received_news_success()
    {
        using var scope = Factory.Services.CreateScope();
        await ReceiveNews(NewsForReceiving());
        var controller = SetupController(scope);
        var result = ((OkObjectResult)controller.GetAllReceived())?.Value as IEnumerable<BloodBankNewsDto>;
        result.Count().ShouldBe(2);
    }
    
    [Fact]
    public void Get_published_news_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        var result = ((OkObjectResult)controller.GetAllPublished())?.Value as IEnumerable<PublishedNewsDto>;
        result.Count().ShouldBe(3);
    }
    
    [Theory]
    [MemberData(nameof(Data))]
    public void Update_news_success(int id, BloodBankNewsDto newsDto, BloodBankNewsState state)
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        var result = ((OkObjectResult)controller.UpdateNews(id,newsDto))?.Value as BloodBankNewsDto;
        result.State.ShouldBe(state);
    }
    

    private async Task ReceiveNews(List<CreateNewsDto> news)
    {
        var rabbitMqConsumer = new BloodBankNewsRabbitMqClient(Factory.Services.CreateScope().ServiceProvider.GetRequiredService<IServiceScopeFactory>());
        await rabbitMqConsumer.StartAsync(CancellationToken.None);
        RabbitMqBloodBankNewsPublisher.Send(news);
        await Task.Delay(1000);
        await rabbitMqConsumer.StopAsync(CancellationToken.None);
    }
    
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 1, NewsForPublishing(), BloodBankNewsState.Published },
            new object[] { 2,  NewsForArchiving(), BloodBankNewsState.Archived },
        };
    
    private static BloodBankNewsDto NewsForPublishing()
    {
        return new BloodBankNewsDto
        {
            Id = 1,
            Title = "Test1",
            Content = "Test1",
            State = BloodBankNewsState.Published
        };
    }
    
    private static BloodBankNewsDto NewsForArchiving()
    {
        return new BloodBankNewsDto
        {
            Id = 2,
            Title = "Test2",
            Content = "Test2",
            State = BloodBankNewsState.Archived
        };
    }

    private List<CreateNewsDto> NewsForReceiving()
    {
        return new List<CreateNewsDto>
        {
            new CreateNewsDto{Content = "asdf",  Title = "asdfgh", Terms = new List<Term>()
            {
                new Term { Start = new DateTime(2023, 3, 12, 11, 30, 0) }
            }},
            new CreateNewsDto{Content = "aff", Title = "asdfgh", Terms = new List<Term>()
            {
                new Term { Start = new DateTime(2023, 3, 12, 11, 30, 0) }
            }},
        };
    }

    private BloodBankNewsController SetupController(IServiceScope scope)
    {
        return new BloodBankNewsController(scope.ServiceProvider.GetRequiredService<IBloodBankNewsService>());
    }
}