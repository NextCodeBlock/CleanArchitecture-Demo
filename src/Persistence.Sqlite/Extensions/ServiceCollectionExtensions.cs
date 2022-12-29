using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CleanArchitectureDemo.Application.Repositories;
using CleanArchitectureDemo.Application.Services;
using CleanArchitectureDemo.Persistence.Sqlite.Context;
using CleanArchitectureDemo.Persistence.Sqlite.Repositories;
using CleanArchitectureDemo.Persistence.Sqlite.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CleanArchitectureDemo.Application.Configurations;

namespace CleanArchitectureDemo.Persistence.Sqlite.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqliteDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
            .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        var databaseConfig = new DatabaseConfig();
        configuration.GetSection(nameof(DatabaseConfig)).Bind(databaseConfig);

        services.AddTransient<IDataSeeder, DataSeeder>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = !string.IsNullOrEmpty(databaseConfig.DatabaseConnection)
                ? databaseConfig.DatabaseConnection
                : "Data Source=TestDatabase.db";
            
            options.UseSqlite(connectionString);
        });

        if (databaseConfig.EnsureDatabaseCreated is true)
            services.EnsureDataStorageCreated();

        if (databaseConfig.ApplyDataSeeder is true)
            services.SeedData();

        return services;
    }

    private static IServiceCollection EnsureDataStorageCreated(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetService<ApplicationDbContext>();
        context?.Database.EnsureCreated();
        return services;
    }

    private static IServiceCollection SeedData(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var initializers = serviceProvider.GetServices<IDataSeeder>();
        var context = serviceProvider.GetService<ApplicationDbContext>();

        if (context==null || !context.Database.CanConnect()) 
            return services;

        foreach (var initializer in initializers)
        {
            initializer.Initialize();
        }
        return services;
    }
}