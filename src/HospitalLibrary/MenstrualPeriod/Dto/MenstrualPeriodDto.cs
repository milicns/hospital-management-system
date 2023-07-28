using System;
namespace HospitalLibrary.MenstrualPeriod.Dto;

public class MenstrualPeriodDto
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int PatientId { get; set; }
    
    public Model.MenstrualPeriod ToEntity()
    {
        return new Model.MenstrualPeriod
        {
            Start = Start,
            End = End,
            PatientId = PatientId,
        };
    }
}