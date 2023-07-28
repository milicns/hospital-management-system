using System.Collections.Generic;
using System.Linq;
using IntegrationLibrary.BloodBank.Model;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace IntegrationLibrary.BloodBank.Repository;

public class BloodBankNewsRepository : IBloodBankNewsRepository
{
    private readonly IntegrationDbContext _context;

    public BloodBankNewsRepository(IntegrationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<BloodBankNews> GetAllReceived()
    {
        return _context.BloodBankNews.Where(n => n.State == BloodBankNewsState.Received).ToList();
    }

    public void AddNews(BloodBankNews news)
    {
        _context.BloodBankNews.Add(news);
        _context.SaveChanges();
    }
    
    public void Update(BloodBankNews news)
    {
        _context.BloodBankNews.Update(news);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }

    public IEnumerable<BloodBankNews> GetAllPublished()
    {
        return _context.BloodBankNews.Where(n => n.State == BloodBankNewsState.Published).ToList();
    }
}

    
