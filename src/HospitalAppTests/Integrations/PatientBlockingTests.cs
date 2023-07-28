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

public class PatientBlockingTests : BaseIntegrationTest
{
    public PatientBlockingTests(TestDatabaseFactory<Startup> factory) : base(factory) { }
    
    [Fact]
    public void Block_patient_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreatePatientController(scope);
        var result = ((OkObjectResult)controller.BlockPatient(3))?.Value as PatientDto;
        result.Blocked.ShouldBeTrue();
    }

    [Fact]
    public void Unblock_patient_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreatePatientController(scope);
        var result = ((OkObjectResult)controller.UnblockPatient(2))?.Value as PatientDto;
        result.Blocked.ShouldBeFalse();
    }
    
    private PatientController CreatePatientController(IServiceScope scope)
    {
        return new PatientController(scope.ServiceProvider.GetRequiredService<IPatientService>(), scope.ServiceProvider.GetRequiredService<IEmailSender>());
    }
}