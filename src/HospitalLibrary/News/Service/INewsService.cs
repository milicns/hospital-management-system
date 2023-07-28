using System.Collections.Generic;
using HospitalLibrary.News.Dto;

namespace HospitalLibrary.News.Service;

public interface INewsService
{
    NewsDto Create(PublishNewsDto publishNewsDto);
    IEnumerable<NewsDto> GetAll();
}