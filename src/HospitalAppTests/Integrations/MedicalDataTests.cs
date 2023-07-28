using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAppTests.Setup;
using HospitalLibrary;
using HospitalLibrary.Email;
using HospitalLibrary.MedicalData.Dto;
using HospitalLibrary.MedicalData.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalAppTests.Integrations;

public class MedicalDataTests : BaseIntegrationTest
{
    public MedicalDataTests(TestDatabaseFactory<Startup> factory) : base(factory) {}
    
    [Fact]
    public void Get_patient_medical_data_record_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        var result = ((OkObjectResult)controller.GetPatientMeasurementsRecord(1))?.Value as IEnumerable<MeasuredDataDto>;
        result.Count().ShouldBe(2);
    }
    
    [Fact]
    public void Record_medical_data_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        var result = ((OkObjectResult)controller.SaveMeasurements(new MeasuredDataDto { PatientId = 1, BloodPressure = 120, BloodSugar = 120, BodyFat = 120, BodyWeight = 120, MeasurementDate = new DateTime(2021, 12, 12) }))?.Value as MeasuredDataDto;
        result.ShouldNotBeNull();
    }

    private MedicalDataController SetupController(IServiceScope scope)
    {
        return new MedicalDataController(scope.ServiceProvider.GetRequiredService<IMedicalDataService>());
    }
}