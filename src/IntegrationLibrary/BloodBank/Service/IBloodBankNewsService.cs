using System.Collections.Generic;
using IntegrationLibrary.BloodBank.Dto;
using IntegrationLibrary.BloodBank.Model;

namespace IntegrationLibrary.BloodBank.Service;

public interface IBloodBankNewsService
{
    IEnumerable<BloodBankNewsDto> GetAllReceived();
    void AddNews(CreateNewsDto news);
    void UpdateNews(BloodBankNewsDto newsDto);
    IEnumerable<PublishedNewsDto> GetAllPublished();
}