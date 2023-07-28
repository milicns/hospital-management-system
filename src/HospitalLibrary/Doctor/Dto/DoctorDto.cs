using System.Collections.Generic;
using HospitalLibrary.Doctor.Model;

namespace HospitalLibrary.Doctor.Dto
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Specialization Specialization { get; set; }
    }
    
}