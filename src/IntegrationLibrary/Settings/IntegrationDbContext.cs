using System;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Model;
using Microsoft.EntityFrameworkCore;

namespace IntegrationLibrary.Settings;

public class IntegrationDbContext : DbContext
{
    
    public DbSet<BloodBankNews> BloodBankNews { get; set; }
    public DbSet<Term> AvailableDonationTerms { get; set; }

    public IntegrationDbContext(DbContextOptions<IntegrationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
    }
    
}
