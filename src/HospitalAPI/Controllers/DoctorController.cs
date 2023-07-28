using System.Linq;
using HospitalLibrary.Doctor.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        
        [HttpGet("general-practitioners")]
        [AllowAnonymous]
        public ActionResult GetGeneralPractitioners()
        {
            return Ok(_doctorService.GetGeneralPractitioners());
        }
        
        [HttpGet("all")]
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            return Ok(_doctorService.GetAll().Select(d=>d.ToDto()));
        }
        
        [HttpGet("chosen/{patientId}")]
        [Authorize(Roles = "Patient")]
        public ActionResult GetChosenDoctorForPatient(int patientId)
        {
            return Ok(_doctorService.GetChosenDoctorForPatient(patientId));
        }
        
    }
}