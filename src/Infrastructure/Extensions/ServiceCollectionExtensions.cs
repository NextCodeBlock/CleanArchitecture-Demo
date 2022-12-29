using CleanArchitectureDemo.Application.Services;
using CleanArchitectureDemo.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureDemo.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddTransient<IDateTimeService, SystemDateTimeService>();
    }
}