using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CleanArchitectureDemo.Application.Services;
using CleanArchitectureDemo.Domain.Contracts;
using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Persistence.Sqlite.Context;

public class ApplicationDbContext : DbContext
{
    private readonly IDateTimeService _dateTimeService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTimeService)
        : base(options)
    {
        _dateTimeService = dateTimeService;
    }

    public DbSet<Customer> Customers { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = _dateTimeService.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedOn = _dateTimeService.Now;
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}