
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Settings;
namespace HospitalLibrary.MedicalData.Repository;

public class MedicalDataRepository : IMedicalDataRepository
{
    private readonly  HospitalDbContext _context;

    public MedicalDataRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public Model.MedicalData Create(Model.MedicalData medicalData)
    {
        var createdMedicalData = _context.MedicalData.Add(medicalData);
        _context.SaveChanges();
        return createdMedicalData.Entity;
    }
    
    public IEnumerable<Model.MedicalData> GetPatientMeasurementsRecord(int patientId)
    {
        return _context.MedicalData.Where(md => md.PatientId == patientId);
    }
}