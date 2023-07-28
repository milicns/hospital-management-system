using System.Collections.Generic;
using System.Linq;
using IntegrationLibrary.BloodBank.Dto;
using IntegrationLibrary.BloodBank.Model;
using IntegrationLibrary.BloodBank.Repository;

namespace IntegrationLibrary.BloodBank.Service;

public class BloodBankNewsService : IBloodBankNewsService
{
    private readonly IBloodBankNewsRepository _newsRepository;
    
    public BloodBankNewsService(IBloodBankNewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }
    
    public IEnumerable<BloodBankNewsDto> GetAllReceived()
    {
        return _newsRepository.GetAllReceived().Select(n => n.ToBloodBankNewsDto());
    }
    
    public void AddNews(CreateNewsDto news)
    {
        _newsRepository.AddNews(news.ToEntity());
    }
    
    public void UpdateNews(BloodBankNewsDto newsDto)
    {
        _newsRepository.Update(newsDto.ToEntity());
    }
    
    public IEnumerable<PublishedNewsDto> GetAllPublished()
    {
        return _newsRepository.GetAllPublished().Select(n => n.ToPublishedNewsDto());
    }
}