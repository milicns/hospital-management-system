using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.MenstrualPeriod.Dto;

namespace HospitalLibrary.MenstrualPeriod.Model;

public class MenstrualPeriod
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key()]
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int PatientId { get; set; }
    public Patient.Model.Patient Patient { get; set; }
    
    public MenstrualPeriodDto ToDto()
    {
        return new MenstrualPeriodDto
        {
            Start = Start,
            End = End,
            PatientId = PatientId,
        };
    }
}