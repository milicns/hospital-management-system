using System;

namespace HospitalLibrary.MedicalData.Dto;
using HospitalLibrary.MedicalData.Model;

public class MeasuredDataDto
{
    public int PatientId { get; set; }
    public double BloodPressure { get; set; }
    public double BloodSugar { get; set; }
    public double BodyFat { get; set; }
    public double BodyWeight { get; set; }
    public DateTime MeasurementDate { get; set; }
        
    public MedicalData ToEntity()
    {
        return new MedicalData
        {
            PatientId = PatientId,
            BloodPressure = BloodPressure,
            BloodSugar = BloodSugar,
            BodyFat = BodyFat,
            BodyWeight = BodyWeight,
            MeasurementDate = MeasurementDate
        };
    }
}