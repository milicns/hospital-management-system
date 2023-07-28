using System;
using System.Linq;
using HospitalLibrary.Settings;

namespace HospitalLibrary.ExaminationReport.Repository;

public class ExaminationReportRepository : IExaminationReportRepository
{
    private readonly HospitalDbContext _context;
    
    public ExaminationReportRepository(HospitalDbContext context)
    {
        _context = context;
    }
    
    public void Create(Model.ExaminationReport examinationReport)
    {
        _context.ExaminationReports.Add(examinationReport);
        _context.SaveChanges();
    }
    
    public Model.ExaminationReport GetById(int id)
    {
        return _context.ExaminationReports.Find(id);
    }
    
    public Model.ExaminationReport GetByExaminationId(int id)
    {
        return _context.ExaminationReports.FirstOrDefault(x => x.ExaminationId == id);
    }
}