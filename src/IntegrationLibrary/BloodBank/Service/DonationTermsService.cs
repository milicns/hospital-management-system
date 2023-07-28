using System.Collections.Generic;
using System.Linq;
using IntegrationLibrary.BloodBank.Model;
using IntegrationLibrary.BloodBank.Repository;

namespace IntegrationLibrary.BloodBank.Service;

public class DonationTermsService : IDonationTermsService
{
    private readonly IDonationTermsRepository _donationTermsRepository;

    public DonationTermsService(IDonationTermsRepository donationTermsRepository)
    {
        _donationTermsRepository = donationTermsRepository;
    }

    public void AddTerms(List<Term> terms)
    {
        foreach (var term in terms)
        {
            _donationTermsRepository.Create(term);
        }
    }

    public IEnumerable<Term> GetAll()
    {
        return _donationTermsRepository.GetAll();
    }
    
    public IEnumerable<ScheduledPatients> GetPatientsScheduledForDonation()
    {
        return _donationTermsRepository.GetAll().SelectMany(term => term.ScheduledPatients);
    }
    
    public void AddPatientToTerm(string patientEmail, int termId)
    {
        var term = _donationTermsRepository.GetAll().FirstOrDefault(term => term.Id == termId);
        term.ScheduledPatients.Add(new ScheduledPatients { PatientEmail = patientEmail });
        _donationTermsRepository.Update(term);
    }
}