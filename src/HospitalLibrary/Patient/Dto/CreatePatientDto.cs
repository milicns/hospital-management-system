using System;
using HospitalLibrary.Patient.Model;
using HospitalLibrary.User.Model;

namespace HospitalLibrary.Patient.Dto
{
    public class CreatePatientDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Ucid { get; set; }
        public string Email { get; set; }
        public DateTime  BirthDate { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
        public string Password { get; set; }
        public BloodType BloodType { get; set; }
        public int ChosenDoctorId { get; set; }
        
        public CreatePatientDto(){}

        public Model.Patient ToEntity()
        {
            return new Model.Patient
            {
                Name = Name,
                Surname = Surname,
                Address = Address,
                BirthDate = BirthDate,
                BloodType = BloodType,
                Email = Email,
                Gender = Gender,
                Ucid = Ucid,
                Password = Password,
                UserRole = UserRole.Patient,
                ChosenDoctorId = ChosenDoctorId
            };
        }
    }
}