using ISM.WebApp.Options;
using ISM.WebApp.Services.ApiClients;
using ISM.WebApp.Services.ApiClients.Interfaces;
using ISM.WebApp.Services.Auth;
using ISM.WebApp.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using AuthenticationService = ISM.WebApp.Services.Auth.AuthenticationService;
using IAuthenticationService = ISM.WebApp.Services.Auth.Interfaces.IAuthenticationService;

namespace ISM.WebApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUiInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorPages();

        services.Configure<ApiClientOptions>(configuration.GetSection(ApiClientOptions.SectionName));
        services.Configure<JwtCookieOptions>(configuration.GetSection(JwtCookieOptions.SectionName));

        services.AddHttpContextAccessor();

        services.AddScoped<IAuthTokenService, AuthTokenService>();
        services.AddScoped<IJwtClaimService, JwtClaimService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddTransient<AuthenticatedHttpClientHandler>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtCookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtCookieAuthenticationDefaults.AuthenticationScheme;
        })
            .AddScheme<AuthenticationSchemeOptions, JwtCookieAuthenticationHandler>(
                JwtCookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                }
            );

        services.AddAuthorizationBuilder()
            .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
            .AddPolicy("JudgeOnly", policy => policy.RequireRole("Judge"))
            .AddPolicy("ParticipantOnly", policy => policy.RequireRole("Participant"));

        services.AddHttpClientClients();

        services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/Admin", "AdminOnly");
            options.Conventions.AuthorizeFolder("/Judge", "JudgeOnly");
            options.Conventions.AuthorizeFolder("/Participant", "ParticipantOnly");
        });

        return services;
    }

    private static IServiceCollection AddHttpClientClients(this IServiceCollection services)
    {
        services.AddHttpClientWithAuth<IEventsApiClient, EventsApiClient>();
        services.AddHttpClientWithAuth<IIdeasApiClient, IdeasApiClient>();
        services.AddHttpClientWithAuth<IEvaluationsApiClient, EvaluationsApiClient>();
        services.AddHttpClientWithAuth<IAuthApiClient, AuthApiClient>();
        return services;
    }

    private static void AddHttpClientWithAuth<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddHttpClient<TInterface, TImplementation>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<ApiClientOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        })
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
    }
}
