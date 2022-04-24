using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace teleperformance_case3.Application;

public static class DependencyInjection
{
    public static void AddApplicationRegistration(this IServiceCollection services)
    {
        var assm = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assm);
        services.AddMediatR(assm);
    }
}