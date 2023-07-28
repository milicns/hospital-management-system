using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Examination.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Examination.Repository;

public class ExaminationRepository : IExaminationRepository
{
    private readonly HospitalDbContext _context;
    
    public ExaminationRepository(HospitalDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Model.Examination> GetAll()
    {
        return _context.Examinations.Include(e=>e.Doctor).Include(e=>e.Patient).ToList();
    }
    
    public Model.Examination GetById(int id)
    {
        return _context.Examinations.Find(id);
    }
    
    public void Update(Model.Examination examination)
    {
        _context.Entry(examination).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }
    
    public IEnumerable<Model.Examination> PatientScheduledExaminations(int patientId)
    {
        return _context.Examinations.Where(e=>e.PatientId == patientId && e.State == ExaminationState.Scheduled);
    }
    
    public IEnumerable<Model.Examination> DoctorScheduledExaminations(int doctorId)
    {
        return _context.Examinations.Where(e=>e.DoctorId == doctorId && e.State == ExaminationState.Scheduled);
    }
    
    public IEnumerable<Model.Examination> PatientPastExaminations(int patientId)
    {
        return _context.Examinations.Where(e=>e.PatientId == patientId && e.State == ExaminationState.Finished);
    }
    
    public Model.Examination Create(Model.Examination examination)
    {
        var createdExamination = _context.Examinations.Add(examination);
        _context.SaveChanges();
        return createdExamination.Entity;
    }
}