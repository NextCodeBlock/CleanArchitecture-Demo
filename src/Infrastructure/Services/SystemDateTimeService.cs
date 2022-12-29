using CleanArchitectureDemo.Application.Services;

namespace CleanArchitectureDemo.Infrastructure.Services;

public class SystemDateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}