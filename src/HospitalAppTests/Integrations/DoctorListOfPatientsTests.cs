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

public class DoctorListOfPatientsTests : BaseIntegrationTest
{
    public DoctorListOfPatientsTests(TestDatabaseFactory<Startup> factory) : base(factory) {}
    
    [Fact]
    public void Get_list_of_patients_of_doctor_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new PatientController(scope.ServiceProvider.GetRequiredService<IPatientService>(),scope.ServiceProvider.GetRequiredService<IEmailSender>());
        var result = ((OkObjectResult)controller.GetListOfPatients(4))?.Value as IEnumerable<PatientDto>;
        result.Count().ShouldBe(3);
    }
}
