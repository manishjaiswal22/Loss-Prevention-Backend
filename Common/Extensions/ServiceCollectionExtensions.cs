using LossPrevention.Api.Data;
using LossPrevention.Api.Repositories.Implementations;
using LossPrevention.Api.Repositories.Interfaces;
using LossPrevention.Api.Services.Implementations;
using LossPrevention.Api.Services.Interfaces;

namespace LossPrevention.Api.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Infrastructure / Database Connection
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

        // Repositories (Data Access Layer)
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();
        services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();

        // Services (Business Logic Layer)
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();
        services.AddScoped<IReportsService, ReportsService>();
        services.AddScoped<IStoreService, StoreService>();

        return services;
    }
}
