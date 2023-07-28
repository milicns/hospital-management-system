using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.DoctorReferral.Repository;

public class DoctorReferralRepository : IDoctorReferralRepository
{
    private readonly HospitalDbContext _context;

    public DoctorReferralRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public Model.DoctorReferral Create(Model.DoctorReferral doctorReferral)
    {
        var createdReferral = _context.DoctorReferrals.Add(doctorReferral);
        _context.SaveChanges();
        return createdReferral.Entity;
    }
    
    public void Update(Model.DoctorReferral doctorReferral)
    {
        _context.Entry(doctorReferral).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }
    
    public IEnumerable<Model.DoctorReferral> PatientNotUsedReferrals(int patientId)
    {
        return _context.DoctorReferrals.Where(dr => dr.PatientId == patientId && !dr.Used);
    }
    
    public Model.DoctorReferral GetById(int id)
    {
        return _context.DoctorReferrals.Find(id);
    }
}
