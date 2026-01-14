using CafePOS.Application.Interfaces;
using CafePOS.Application.Interfaces.Auth;
using CafePOS.Application.Services.Auth;
using CafePOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CafePOS.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // =======================
        // Database (MySQL)
        // =======================
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(
                    configuration.GetConnectionString("DefaultConnection")
                )
            )
        );

        // =======================
        // Services
        // =======================
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
