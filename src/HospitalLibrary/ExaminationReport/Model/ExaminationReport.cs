using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.ExaminationReport.Dto;

namespace HospitalLibrary.ExaminationReport.Model;

public class ExaminationReport
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }
    public int ExaminationId { get; set; }
    public Examination.Model.Examination Examination { get; set; }
    public DateTime Date { get; set; }
    public string Diagnosis { get; set; }
    public string Treatment { get; set; }

    public ExaminationReportDto ToDto()
    {
        return new ExaminationReportDto
        {
            Id = Id,
            ExaminationId = ExaminationId,
            Diagnosis = Diagnosis,
            Treatment = Treatment,
            Date = Date
        };
    }
}