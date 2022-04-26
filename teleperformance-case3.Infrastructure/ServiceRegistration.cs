using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using teleperformance_case3.Infrastructure.Persistence;

namespace teleperformance_case3.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureRegistration(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("TeleperformanceDb"));
    }
}