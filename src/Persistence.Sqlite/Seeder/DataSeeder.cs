using Microsoft.Extensions.Logging;
using CleanArchitectureDemo.Application.Services;
using CleanArchitectureDemo.Persistence.Sqlite.Context;
using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Persistence.Sqlite.Seeder;

public class DataSeeder : IDataSeeder
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DataSeeder> _logger;

    public DataSeeder(ApplicationDbContext dbContext, ILogger<DataSeeder> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public void Initialize()
    {
        AddCustomers();
        _dbContext.SaveChanges();
    }

    private void AddCustomers()
    {
        Task.Run(() =>
        {
            if (!_dbContext.Customers.Any())
            {
                IEnumerable<Customer> customers = new List<Customer>()
                {
                    new Customer
                    {
                        Name = "Amin Ziagham",
                        Email = "amin.ziagham@gmail.com",
                        Website = "https://nextcodeblock.com",
                        IsActive = true
                    }
                };
                _dbContext.Customers.AddRange(customers);
            }
        }).GetAwaiter().GetResult();
    }
}