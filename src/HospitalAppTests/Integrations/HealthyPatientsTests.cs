using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAppTests.Setup;
using HospitalLibrary.Email;
using HospitalLibrary.Patient.Dto;
using HospitalLibrary.Patient.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalAppTests.Integrations;

public class HealthyPatientsTests : BaseIntegrationTest
{
    public HealthyPatientsTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    [Fact]
    public void Get_healthy_patients_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new PatientController(scope.ServiceProvider.GetRequiredService<IPatientService>(), scope.ServiceProvider.GetRequiredService<IEmailSender>());
        var result = ((OkObjectResult)controller.GetHealthyPatients())?.Value as IEnumerable<PatientDto>;
        result.Count().ShouldBe(1);
    }
    
}