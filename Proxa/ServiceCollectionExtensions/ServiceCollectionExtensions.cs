using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proxa.Services;
using Proxa.Data;
using Proxa.Middleware;

namespace Proxa.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<AccountManagementService>();
            services.AddScoped<APIKeyManagementService>();
            services.AddScoped<ProxyCheckingService>();
            services.AddScoped<ListServingService>();

            services.AddScoped<AuthMiddleware>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RateLimitingMiddleware>();

            return services;
        }
    }
}
