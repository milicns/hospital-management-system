using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.News.Dto;
using HospitalLibrary.News.Repository;

namespace HospitalLibrary.News.Service;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;

    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public NewsDto Create(PublishNewsDto publishNewsDto)
    {
        return _newsRepository.Create(publishNewsDto.ToEntity()).ToDto();
    }

    public IEnumerable<NewsDto> GetAll()
    {
        return _newsRepository.GetAll().Select(n=>n.ToDto());
    }
}