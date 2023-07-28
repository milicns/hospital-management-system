using System.Collections.Generic;

namespace HospitalLibrary.News.Repository;

public interface INewsRepository
{
    Model.News Create(Model.News news);
    IEnumerable<Model.News> GetAll();
}