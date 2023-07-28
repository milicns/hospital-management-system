using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAppTests.Setup;
using HospitalLibrary.Doctor.Model;
using HospitalLibrary.DoctorReferral.Dto;
using HospitalLibrary.DoctorReferral.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalAppTests.Integrations;

public class DoctorReferralTests : BaseIntegrationTest
{
    public DoctorReferralTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    [Fact]
    public void Create_Referral_Success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new DoctorReferralController(scope.ServiceProvider.GetRequiredService<IDoctorReferralService>());
        var result = ((OkObjectResult)controller.CreateReferral(CreateReferralDto()))?.Value as DoctorReferralDto;
        result.ShouldNotBeNull();
    }
    
    private CreateReferralDto CreateReferralDto()
    {
        return new CreateReferralDto()
        {
            PatientId = 1,
            DoctorId = 4,
            Specialization = Specialization.Cardiologist,
        };
    }
}