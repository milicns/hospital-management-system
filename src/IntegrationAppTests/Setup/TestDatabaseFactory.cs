
using IntegrationAPI;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Model;
using IntegrationLibrary.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationAppTests.Setup;

public class TestDatabaseFactory<TStartup> : WebApplicationFactory<Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            using var scope = BuildServiceProvider(services).CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<IntegrationDbContext>();
            InitializeDatabase(db);
                   
        });
    }
    
    private static ServiceProvider BuildServiceProvider(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<IntegrationDbContext>));
        services.Remove(descriptor);

        services.AddDbContext<IntegrationDbContext>(option => option.UseNpgsql(CreateConnectionStringForTest()));
        return services.BuildServiceProvider();
    }
    
    private static string CreateConnectionStringForTest()
    {
        return "Host=localhost;Database=IntegrationDbTest;Username=postgres;Password=ftn;";
    }

    private static void InitializeDatabase(IntegrationDbContext context)
    {
        
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"BloodBankNews\";");
        context.BloodBankNews.Add(new BloodBankNews
        {
            Title = "Test1",
            Content = "Test1",
            State = BloodBankNewsState.Received
        });
        context.BloodBankNews.Add(new BloodBankNews
        {
            Title = "Test2",
            Content = "Test2",
            State = BloodBankNewsState.Received
        });
        context.BloodBankNews.Add(new BloodBankNews
        {
            Title = "Test3",
            Content = "Test3",
            State = BloodBankNewsState.Published
        });
        context.BloodBankNews.Add(new BloodBankNews
        {
            Title = "Test4",
            Content = "Test4",
            State = BloodBankNewsState.Published
        });
        context.SaveChanges();
    }
}