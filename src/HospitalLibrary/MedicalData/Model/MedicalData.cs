using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.MedicalData.Dto;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.MedicalData.Model;

public class MedicalData
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key()]
    public int Id { get; set; }
    public double BloodPressure { get; set; }
    public double BloodSugar { get; set; }
    public double BodyFat { get; set; }
    public double BodyWeight { get; set; }
    public DateTime MeasurementDate { get; set; }
    public int PatientId { get; set; }
    public Patient.Model.Patient Patient { get; set; }

    public MeasuredDataDto ToDto()
    {
        return new MeasuredDataDto
        {
            BloodPressure = BloodPressure,
            BloodSugar = BloodSugar,
            BodyFat = BodyFat,
            BodyWeight = BodyWeight,
            MeasurementDate = MeasurementDate
        };
           
    }
}