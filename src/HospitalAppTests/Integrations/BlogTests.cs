using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAppTests.Setup;
using HospitalLibrary.Blog.Dto;
using HospitalLibrary.Blog.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalAppTests.Integrations;

public class BlogTests : BaseIntegrationTest
{
    public BlogTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public void Get_all_blogs_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>());
        var result = ((OkObjectResult)controller.GetAllBlogs())?.Value as IEnumerable<BlogDto>;
        result.Count().ShouldBe(1);
    }
    
    [Fact]
    public void Write_blog_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>());
        var result = ((OkObjectResult)controller.WriteBlog(BlogForWriting()))?.Value as BlogDto;
        result?.Id.ShouldNotBe(0);
    }
    
    private CreateBlogDto BlogForWriting()
    {
        return new CreateBlogDto
        {
            Title = "Test Blog",
            Content = "Test Blog Content",
            AuthorId = 4
        };
    }
}