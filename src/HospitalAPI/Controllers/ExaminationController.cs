
using HospitalLibrary.Examination.Dto;
using HospitalLibrary.Examination.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[Route("api/examination")]
[ApiController]
public class ExaminationController : ControllerBase
{
    private readonly IExaminationService _examinationService;

    public ExaminationController(IExaminationService examinationService)
    {
        _examinationService = examinationService;
    }
    
    [HttpPost]
    [Authorize(Roles = "Patient")]
    public ActionResult ScheduleExamination(RecommendedExaminationDto recommendedExaminationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_examinationService.ScheduleExamination(recommendedExaminationDto));
    }

    [HttpGet("patient-scheduled/{patientId}")]
    [Authorize(Roles = "Patient")]
    public ActionResult PatientScheduledExaminations(int patientId)
    {
        return Ok(_examinationService.PatientScheduledExaminations(patientId));
    }
    
    [HttpGet("doctor-scheduled/{doctorId}")]
    [Authorize(Roles = "Doctor")]
    public ActionResult DoctorScheduledExaminations(int doctorId)
    {
        return Ok(_examinationService.DoctorScheduledExaminations(doctorId));
    }
    
    [HttpGet("past/{patientId}")]
    public ActionResult PatientPastExaminations(int patientId)
    {
        return Ok(_examinationService.PatientPastExaminations(patientId));
    }
    
    [HttpPut("cancel/{id}")]
    [Authorize(Roles = "Patient")]
    public ActionResult CancelExamination(int id)
    {
        return Ok(_examinationService.CancelExamination(id));
    }

    [HttpPost("recommend")]
    [Authorize(Roles = "Patient")]
    public ActionResult RecommendAvailableTerm(SearchExaminationDto searchExaminationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var recommendedExamination = _examinationService.RecommendAvailableTerm(searchExaminationDto);
        
        return Ok(recommendedExamination);
    }
}