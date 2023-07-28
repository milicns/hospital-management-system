using System.Collections.Generic;
using IntegrationLibrary.BloodBank.Model;

namespace IntegrationLibrary.BloodBank.Repository;

public interface IBloodBankNewsRepository
{
    IEnumerable<BloodBankNews> GetAllReceived();
    void AddNews(BloodBankNews news);
    void Update(BloodBankNews news);
    IEnumerable<BloodBankNews> GetAllPublished();
    
}