using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAppTests.Setup;
using HospitalLibrary;
using HospitalLibrary.MenstrualPeriod.Dto;
using HospitalLibrary.MenstrualPeriod.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalAppTests.Integrations;

public class MenstrualPeriodTests : BaseIntegrationTest
{
    public MenstrualPeriodTests(TestDatabaseFactory<Startup> factory) : base(factory) {}
    
    [Fact]
    public void Get_past_menstrual_periods_of_female_patient_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        var result = ((OkObjectResult)controller.GetPeriodsOfFemalePatient(2))?.Value as IEnumerable<MenstrualPeriodDto>;
        result.Count().ShouldBe(1);
    }
    
    [Fact]
    public void Save_menstrual_period_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        var result = ((OkObjectResult)controller.SaveMenstrualPeriod(new MenstrualPeriodDto{PatientId = 2,Start = new DateTime(2023,3,3),End = new DateTime(2023,3,8) }))?.Value as MenstrualPeriodDto;
        result.ShouldNotBeNull();
    }
    
    private MenstrualPeriodController SetupController(IServiceScope scope)
    {
        return new MenstrualPeriodController(scope.ServiceProvider.GetRequiredService<IMenstrualPeriodService>());
    }
    
}