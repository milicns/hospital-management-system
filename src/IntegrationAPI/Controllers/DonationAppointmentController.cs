using IntegrationLibrary.BloodBank.Dto;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.Grpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAPI.Controllers;

[Route("api/donation")]
[ApiController]
public class DonationAppointmentController : ControllerBase
{
    private readonly IBloodBankGrpc _bloodBankGrpc;
    private readonly IDonationTermsService _donationTermsService;
    
    public DonationAppointmentController(IBloodBankGrpc bloodBankGrpc, IDonationTermsService donationTermsService)
    {
        _bloodBankGrpc = bloodBankGrpc;
        _donationTermsService = donationTermsService;
    }
    
    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public ActionResult ScheduleDonation(ScheduleDonationAppointmentDto scheduleAppointmentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(_bloodBankGrpc.ScheduleDonation(scheduleAppointmentDto));
    }
    
    [HttpGet("terms")]
    [Authorize(Roles = "Doctor")]
    public ActionResult GetAvailable()
    {
        return Ok(_donationTermsService.GetAll());
    }
    
    [HttpGet("scheduled/patients")]
    [Authorize(Roles = "Doctor")]
    public ActionResult GetPatientsScheduledForDonation()
    {
        return Ok(_donationTermsService.GetPatientsScheduledForDonation());
    }

}