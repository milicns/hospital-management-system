using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAppTests.Setup;
using HospitalLibrary.Auth.Dto;
using HospitalLibrary.Auth.Interface;
using HospitalLibrary.Exceptions;
using HospitalLibrary.ValidationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalAppTests.Integrations;

public class LoginTests : BaseIntegrationTest
{

    public LoginTests(TestDatabaseFactory<Startup> factory) : base(factory) {}
    
    [Fact]
    public void Login_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateAuthController(scope);
        var result = ((OkObjectResult)controller.Login(new LoginDto { Email="milicvasilije9@gmail.com", Password="asd" }))?.Value as AuthResponse;
        result.Token.ShouldNotBeNull();
    }
    
    [Fact]
    public void Login_failed()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateAuthController(scope);
        var result = ((ObjectResult)controller.Login(new LoginDto { Email="marko@gmail.com", Password="asdf" }))?.Value as ErrorObject;
        result.Message.ShouldBe("Account with that email or password doesn't exists");
    }

    [Fact]
    public void Login_failed_user_blocked()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateAuthController(scope);
        var result = ((ObjectResult)controller.Login(new LoginDto { Email="marko@gmail.com", Password="asd" }))?.Value as ErrorObject;
        result.Message.ShouldBe("Your account is blocked from logging in. Contact administrator for more informations.");
    }

    private AuthController CreateAuthController(IServiceScope scope){ 
        return new AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>(),scope.ServiceProvider.GetRequiredService<IValidationService>());
    }
    
}