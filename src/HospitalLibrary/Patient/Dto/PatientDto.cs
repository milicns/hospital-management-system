using System;
using HospitalLibrary.Patient.Model;
using HospitalLibrary.User.Model;

namespace HospitalLibrary.Patient.Dto
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Ucid { get; set; }
        public string Email { get; set; }
        public DateTime  BirthDate { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
        public BloodType BloodType { get; set; }
        public int ChosenDoctorId { get; set; }
        public bool Blocked { get; set; }
        
    }
}