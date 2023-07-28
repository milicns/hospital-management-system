using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAppTests.Setup;
using HospitalLibrary.Email;
using HospitalLibrary.Exceptions;
using HospitalLibrary.Patient.Dto;
using HospitalLibrary.Patient.Model;
using HospitalLibrary.Patient.Service;
using HospitalLibrary.User.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalAppTests.Integrations;

public class PatientRegistrationTests : BaseIntegrationTest
{
    public PatientRegistrationTests(TestDatabaseFactory<Startup> factory) : base(factory) {}
    
    [Fact]
    public void Register_patient_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        var result = ((OkObjectResult)controller.RegisterPatient(PatientForRegistration()))?.Value as PatientDto;
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void Register_patient_failed()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        var result = ((BadRequestObjectResult)controller.RegisterPatient(PatientForRegistrationFail()))?.Value as ErrorObject;
        result.ShouldBeOfType<ErrorObject>("Email is already taken");
    }
    
    
    private PatientController SetupController(IServiceScope scope){ 
        return new PatientController(scope.ServiceProvider.GetRequiredService<IPatientService>(),scope.ServiceProvider.GetRequiredService<IEmailSender>());
    }

    private CreatePatientDto PatientForRegistration()
    {
        Address address = new Address{Country = "Srbija",City = "Novi Sad",Street = "Jevrejska",Number = 11};
        return new CreatePatientDto
        {
            Name= "Vasilije", Surname = "Milic",Ucid = 32234,Email = "milic@gmail.com",BirthDate = new DateTime(2000, 12, 12),Address = address,
            Password = "asdf", BloodType = BloodType.AbPositive, ChosenDoctorId = 2
        };
    }
    
    private CreatePatientDto PatientForRegistrationFail()
    {
        Address address = new Address{Country = "Srbija",City = "Novi Sad",Street = "Dunavska",Number = 5};
        return new CreatePatientDto
        {
            Name= "Marko", Surname = "Milic",Ucid = 64334,Email = "marko@gmail.com",BirthDate = new DateTime(2000, 12, 12),Address = address,
            Password = "asdf", BloodType = BloodType.AbPositive, ChosenDoctorId = 2
        };
    }
}