using System;

namespace HospitalLibrary.ExaminationReport.Dto;

public class ExaminationReportDto
{
    public int Id { get; set; }
    public int ExaminationId { get; set; }
    public string Diagnosis { get; set; }
    public string Treatment { get; set; }
    public DateTime Date { get; set; }
}