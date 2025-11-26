using ISM.Application.Extensions.DependencyInjection;
using ISM.Infrastructure.Identity.Extensions.DependencyInjection;
using ISM.Infrastructure.Persistence.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ISM.WebFramework.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddInfrastructureIdentity(configuration);
        services.AddApplication();

        services.AddControllers();

        services.AddApiVersioningConfigured();
        services.AddSwaggerConfigured();

        return services;
    }
}
