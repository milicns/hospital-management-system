
using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAppTests.Setup;
using HospitalLibrary.Examination.Dto;
using HospitalLibrary.Examination.Model;
using HospitalLibrary.Examination.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalAppTests.Integrations;

public class ExaminationTests : BaseIntegrationTest
{
    public ExaminationTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    [Fact]
    public void Examination_schedule_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new ExaminationController(scope.ServiceProvider.GetRequiredService<IExaminationService>());
        var result = ((OkObjectResult)controller.ScheduleExamination(ScheduleExaminationDtoSuccess()))?.Value as ExaminationDto;
        result.Id.ShouldNotBe(0);
    }
    
    [Fact]
    public void Get_Scheduled_Examinations_Success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new ExaminationController(scope.ServiceProvider.GetRequiredService<IExaminationService>());
        var result = ((OkObjectResult)controller.PatientScheduledExaminations(3))?.Value as IEnumerable<ExaminationDto>;
        result.Count().ShouldBe(2);
    }

    [Fact]
    public void Cancel_Examination_Success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new ExaminationController(scope.ServiceProvider.GetRequiredService<IExaminationService>());
        var result = ((OkObjectResult)controller.CancelExamination(2))?.Value as ExaminationDto;
        result.State.ShouldBe(ExaminationState.Canceled);
    }
    
    private RecommendedExaminationDto ScheduleExaminationDtoSuccess()
    {
        return new RecommendedExaminationDto
        {
            DoctorId = 2,
            PatientId = 1,
            ExaminationTerm = new DateTime(2023, 5, 1, 8, 0, 0)
        };
    }
}