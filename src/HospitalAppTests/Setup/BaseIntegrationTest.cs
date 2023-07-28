using HospitalAPI;

namespace HospitalAppTests.Setup;

public class BaseIntegrationTest : IClassFixture<TestDatabaseFactory<Startup>>
{
    protected TestDatabaseFactory<Startup> Factory { get; set; }
    
    public BaseIntegrationTest(TestDatabaseFactory<Startup> factory)
    {
        Factory = factory;
    }
}