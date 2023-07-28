using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Settings;

namespace HospitalLibrary.News.Repository;

public class NewsRepository : INewsRepository
{
    private readonly  HospitalDbContext _context;

    public NewsRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public Model.News Create(Model.News news)
    {
        var createdNews = _context.News.Add(news);
        _context.SaveChanges();
        return createdNews.Entity;
    }

    public IEnumerable<Model.News> GetAll()
    {
        return _context.News.ToList();
    }
}