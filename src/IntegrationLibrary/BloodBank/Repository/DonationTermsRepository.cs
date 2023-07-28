using System.Collections.Generic;
using System.Linq;
using IntegrationLibrary.BloodBank.Model;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace IntegrationLibrary.BloodBank.Repository;

public class DonationTermsRepository : IDonationTermsRepository
{
    private readonly IntegrationDbContext _context;
    
    public DonationTermsRepository(IntegrationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Term> GetAll()
    {
        return _context.AvailableDonationTerms.ToList();
    }
    
    public void Create(Term term)
    {
        _context.AvailableDonationTerms.Add(term);
        _context.SaveChanges();
    }
    
    public void Update(Term term)
    {
        _context.Entry(term).State = EntityState.Modified;
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }
}