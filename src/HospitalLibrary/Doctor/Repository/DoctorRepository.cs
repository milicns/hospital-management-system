using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Doctor.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Doctor.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDbContext _context;

        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }
        
        public Doctor.Model.Doctor GetById(int id)
        {
            return _context.Doctors.Include(x => x.Patients).FirstOrDefault(i => i.Id == id);
        }
        
        public void Update(Doctor.Model.Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        
        public IEnumerable<Model.Doctor> GetAll()
        {
           return _context.Doctors.Include(d=>d.Examinations).ToList();
        }
        
        public IEnumerable<Model.Doctor> GetGeneralPractitioners()
        {
            return _context.Doctors.Where(d => d.Specialization == Specialization.Generalist).ToList();
        }
        
        public Model.Doctor GetChosenDoctorForPatient(int patientId)
        {
            return _context.Doctors.FirstOrDefault(d => d.Patients.Any(p => p.Id == patientId));
        }
       
    }
}