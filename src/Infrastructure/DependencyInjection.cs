using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Infrastructure.Ai;
using AiJobEx1.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AiJobEx1.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddScoped<IAiProvider, MockAiProvider>();
        services.AddScoped<IAuditTrailService, AuditTrailService>();

        services.AddLogging(builder => builder.AddSerilog(dispose: true));

        return services;
    }
}
