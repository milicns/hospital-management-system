using Microsoft.EntityFrameworkCore;

namespace IntegrationLibrary.BloodBank.Model;

[Owned]
public class ScheduledPatients
{
    public string PatientEmail { get; set; }
}