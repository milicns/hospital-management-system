
using HospitalLibrary.DoctorReferral.Dto;
using HospitalLibrary.DoctorReferral.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[Route("api/referral")]
[ApiController]
public class DoctorReferralController : ControllerBase
{
    private readonly IDoctorReferralService _doctorReferralService;
    
    public DoctorReferralController(IDoctorReferralService doctorReferralService)
    {
        _doctorReferralService = doctorReferralService;
    }
    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public ActionResult CreateReferral(CreateReferralDto createReferralDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_doctorReferralService.Create(createReferralDto));
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Patient")]
    public ActionResult GetById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_doctorReferralService.GetById(id).ToDto());
    }
    
    
    [HttpGet("not-used/{patientId}")]
    [Authorize(Roles = "Patient")]
    public ActionResult GetPatientReferrals(int patientId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_doctorReferralService.PatientNotUsedReferrals(patientId));
    }
    
}