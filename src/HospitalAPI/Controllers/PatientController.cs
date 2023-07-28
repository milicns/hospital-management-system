using System;
using HospitalLibrary.Email;
using HospitalLibrary.Exceptions;
using HospitalLibrary.Patient.Dto;
using HospitalLibrary.Patient.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IEmailSender _emailSender;

        public PatientController(IPatientService patientService, IEmailSender emailSender)
        {
            _patientService = patientService;
            _emailSender = emailSender;
        }
        
        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult RegisterPatient(CreatePatientDto createPatientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PatientDto createdPatient = _patientService.RegisterPatient(createPatientDto);
                //_emailSender.SendEmail(new Message(new string[] {createdPatient.Email}, "Welcome to Hospital", "You have been successfully registered. Welcome to our hospital"));
                return Ok(createdPatient);
                
            }catch(Exception e)
            {
                return BadRequest(new ErrorObject{Message = e.Message});
            }

        }
        
        [HttpGet("malicious")]
        [Authorize(Roles = "SystemAdministrator")]
        public ActionResult GetMaliciousPatients()
        {
            return Ok(_patientService.MaliciousPatients());
        }
        
        [HttpGet("healthy")]
        [Authorize(Roles = "Doctor")]
        public ActionResult GetHealthyPatients()
        {
            return Ok(_patientService.HealthyPatients());
        }
        
        [HttpGet("blocked")]
        [Authorize(Roles = "SystemAdministrator")]
        public ActionResult GetBlockedPatients()
        {
            return Ok(_patientService.BlockedPatients());
        }

        [HttpPut("block/{id}")]
        [Authorize(Roles = "SystemAdministrator")]
        public ActionResult BlockPatient(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blockedPatient = _patientService.Block(id);
            _emailSender.SendEmail(new Message(new string[] {blockedPatient.Email}, "Blocked account", "You have been blocked from using our services due to often canceling of scheduled examinations."));
            return Ok(blockedPatient);
        }
        
        
        [HttpPut("unblock/{id}")]
        [Authorize(Roles = "SystemAdministrator")]
        public ActionResult UnblockPatient(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_patientService.Unblock(id));
        }
        
        [HttpGet("list/{doctorId}")]
        [Authorize(Roles = "Doctor")]
        public ActionResult GetListOfPatients(int doctorId)
        {
            return Ok(_patientService.GetDoctorsListOfPatients(doctorId));
        }

    }
}