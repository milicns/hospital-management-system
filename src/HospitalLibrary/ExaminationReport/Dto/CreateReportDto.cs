using System;
using HospitalLibrary.MedicalData.Dto;

namespace HospitalLibrary.ExaminationReport.Dto;

public class CreateReportDto
{
    public int ExaminationId { get; set; }
    public string Diagnosis { get; set; }
    public string Treatment { get; set; }
    public DateTime Date { get; set; }
    public MeasuredDataDto MeasuredData { get; set; }
    
    public Model.ExaminationReport ToEntity()
    {
        return new Model.ExaminationReport
        {
            ExaminationId = ExaminationId,
            Diagnosis = Diagnosis,
            Treatment = Treatment,
            Date = Date
        };
    }
}