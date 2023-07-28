
using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAppTests.Setup;
using HospitalLibrary.ExaminationReport.Dto;
using HospitalLibrary.ExaminationReport.Service;
using HospitalLibrary.MedicalData.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalAppTests.Integrations;

public class ExaminationReportTests : BaseIntegrationTest
{
    public ExaminationReportTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    
    [Fact]
    public void Create_Examination_Report_Success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new ExaminationReportController(scope.ServiceProvider.GetRequiredService<IExaminationReportService>());
        var result = ((OkObjectResult)controller.CreateExaminationReport(CreateReportDto()))?.Value as ExaminationReportDto;
        result.ShouldNotBeNull();
    }

    [Fact]
    public void Create_Examination_Report_With_Medical_Data()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new ExaminationReportController(scope.ServiceProvider.GetRequiredService<IExaminationReportService>());
        var result = ((OkObjectResult)controller.CreateExaminationReport(CreateReportDto_WithMedicalData()))?.Value as ExaminationReportDto;
        result.ShouldNotBeNull();
    }
    
    
    private CreateReportDto CreateReportDto()
    {
        return new CreateReportDto
        {
            ExaminationId = 2,
            Diagnosis = "Diagnosis",
            Treatment = "Treatment",
            Date = DateTime.Now
        };
    }
    
    
    private CreateReportDto CreateReportDto_WithMedicalData()
    {
        return new CreateReportDto
        {
            ExaminationId = 3,
            Diagnosis = "Diagnosis",
            Treatment = "Treatment",
            Date = DateTime.Now,
            MeasuredData = new MeasuredDataDto
            {
                BloodPressure = 120,
                BloodSugar = 121,
                BodyFat = 80,
                BodyWeight = 35,
                MeasurementDate = DateTime.Now
            }
        };
    }
}