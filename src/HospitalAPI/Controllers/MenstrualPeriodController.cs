using HospitalLibrary.MenstrualPeriod.Dto;
using HospitalLibrary.MenstrualPeriod.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;
[Route("api/period")]
[ApiController]
public class MenstrualPeriodController : ControllerBase
{
    private readonly IMenstrualPeriodService _menstrualPeriodService;
    
    public MenstrualPeriodController(IMenstrualPeriodService menstrualPeriodService)
    {
        _menstrualPeriodService = menstrualPeriodService;
    }
    
    [HttpGet("all/{patientId}")]
    [Authorize(Roles = "Patient")]
    public ActionResult GetPeriodsOfFemalePatient(int patientId)
    {
        return Ok(_menstrualPeriodService.GetPatientMenstrualPeriods(patientId));
    }
        
    [HttpPost]
    [Authorize(Roles = "Patient")]
    public ActionResult SaveMenstrualPeriod(MenstrualPeriodDto periodDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_menstrualPeriodService.SaveMenstrualPeriod(periodDto));
    }

}