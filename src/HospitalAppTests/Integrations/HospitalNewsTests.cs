using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAppTests.Setup;
using HospitalLibrary.News.Dto;
using HospitalLibrary.News.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalAppTests.Integrations;

public class HospitalNewsTests : BaseIntegrationTest
{
    public HospitalNewsTests(TestDatabaseFactory<Startup> factory):base(factory){}
    
    [Fact]
    public void Publish_news_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new NewsController(scope.ServiceProvider.GetRequiredService<INewsService>());
        var result = ((OkObjectResult)controller.PublishNews(PublishNewsDto()))?.Value as NewsDto;
        result.ShouldNotBeNull();
    }


    private PublishNewsDto PublishNewsDto()
    {
        return new PublishNewsDto()
        {
            Title = "Test",
            Content = "Test",
            AuthorId = 5,
        };
    }

}