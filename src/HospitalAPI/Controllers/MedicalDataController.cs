using HospitalLibrary.MedicalData.Dto;
using HospitalLibrary.MedicalData.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[Route("api/medical-data")]
[ApiController]
public class MedicalDataController : ControllerBase
{
    private readonly IMedicalDataService _medicalDataService;

    public MedicalDataController(IMedicalDataService medicalDataService)
    {
        _medicalDataService = medicalDataService;
    }

    [HttpPost]
    [Authorize(Roles = "Patient")]
    public ActionResult SaveMeasurements(MeasuredDataDto measuredDataDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return Ok(_medicalDataService.Measure(measuredDataDto));
    }
    
    [HttpGet("record/{patientId}")]
    [Authorize(Roles = "Patient,Doctor")]
    public ActionResult GetPatientMeasurementsRecord(int patientId)
    {
        return Ok(_medicalDataService.GetPatientMeasurementsRecord(patientId));
    }
}