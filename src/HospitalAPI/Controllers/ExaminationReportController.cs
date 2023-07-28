
using HospitalLibrary.ExaminationReport.Dto;
using HospitalLibrary.ExaminationReport.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[Route("api/examination-report")]
[ApiController]
public class ExaminationReportController : ControllerBase
{
    private readonly IExaminationReportService _examinationReportService;
    
    public ExaminationReportController(IExaminationReportService examinationReportService)
    {
        _examinationReportService = examinationReportService;
    }
    
    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public ActionResult CreateExaminationReport(CreateReportDto createReportDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_examinationReportService.Create(createReportDto));
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Patient")]
    public ActionResult GetReport(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_examinationReportService.GetById(id));
    }
    
}