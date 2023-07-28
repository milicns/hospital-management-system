using System;
using IntegrationLibrary.BloodBank.Model;

namespace IntegrationLibrary.BloodBank.Dto;

public class ScheduleDonationAppointmentDto
{
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PatientEmail { get; set; }
    public int TermId { get; set; }
    public PatientBloodType PatientBloodType { get; set; }
}