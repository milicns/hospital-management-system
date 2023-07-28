using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Settings;

namespace HospitalLibrary.MenstrualPeriod.Repository;

public class MenstrualPeriodRepository : IMenstrualPeriodRepository
{
    private readonly HospitalDbContext _context;
    
    public MenstrualPeriodRepository(HospitalDbContext context)
    {
        _context = context;
    }
    
    public Model.MenstrualPeriod Create(Model.MenstrualPeriod menstrualPeriod)
    {
        var createdPeriod = _context.MenstrualPeriods.Add(menstrualPeriod);
        _context.SaveChanges();
        return createdPeriod.Entity;
    }
    
    public IEnumerable<Model.MenstrualPeriod> GetPatientMenstrualPeriods(int patientId)
    {
        return _context.MenstrualPeriods.Where(mp=>mp.PatientId == patientId);
    }

}