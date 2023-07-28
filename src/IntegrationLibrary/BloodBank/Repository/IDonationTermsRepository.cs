using System.Collections.Generic;
using IntegrationLibrary.BloodBank.Model;

namespace IntegrationLibrary.BloodBank.Repository;

public interface IDonationTermsRepository
{
    IEnumerable<Term> GetAll();
    void Create(Term term);
    void Update(Term term);
}