using System.Collections.Generic;
using IntegrationLibrary.BloodBank.Model;

namespace IntegrationLibrary.BloodBank.Service;

public interface IDonationTermsService
{
    void AddTerms(List<Term> terms);
    IEnumerable<Term> GetAll();
    IEnumerable<ScheduledPatients> GetPatientsScheduledForDonation();
    void AddPatientToTerm(string patientEmail, int termId);
}