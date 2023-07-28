

using System.Collections.Generic;
using HospitalLibrary.Doctor.Dto;

namespace HospitalLibrary.Doctor.Model;

    public class Doctor : User.Model.User
    {
        public Specialization Specialization { get; set; }
        public List<Patient.Model.Patient> Patients { get; set; }
        public List<Blog.Model.Blog> WrittenBlogs { get; set; }
        public List<Examination.Model.Examination> Examinations { get; set; }
        
        public DoctorDto ToDto()
        {
            return new DoctorDto
            {
                Email = Email, Id = Id, Name = Name, Surname = Surname, Specialization = Specialization
            };
        }
        
    }
