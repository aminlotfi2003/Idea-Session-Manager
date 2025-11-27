using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Common.Abstractions.Repositories.Application;
using ISM.Infrastructure.Persistence.Configurations;
using ISM.Infrastructure.Persistence.Contexts;
using ISM.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ISM.Infrastructure.Persistence.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Default")
                   ?? throw new InvalidOperationException("ConnectionStrings: Default is missing.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connection, sql =>
            {
                sql.MigrationsHistoryTable("__EFMigrationsHistory", Schemas.Application);
                sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IInnovationEventRepository, InnovationEventRepository>();
        services.AddScoped<IIdeaRepository, IdeaRepository>();
        services.AddScoped<IParticipantProfileRepository, ParticipantProfileRepository>();
        services.AddScoped<IJudgeRepository, JudgeRepository>();
        services.AddScoped<IEvaluationCriteriaRepository, EvaluationCriteriaRepository>();
        services.AddScoped<IIdeaEvaluationRepository, IdeaEvaluationRepository>();
        services.AddScoped<IEvaluationScoreRepository, EvaluationScoreRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
