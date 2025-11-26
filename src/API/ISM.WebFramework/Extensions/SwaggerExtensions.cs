using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ISM.WebFramework.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfigured(this IServiceCollection services, Action<SwaggerGenOptions>? configure = null)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Idea Session Manager API",
                Version = "v1",
                Description = "ASP.NET Core Idea Session Manager"
            });

            // JWT Auth Section
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // For additional customization if needed
            configure?.Invoke(opt);
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfigured(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
            foreach (var desc in provider.ApiVersionDescriptions)
            {
                opt.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
            }
            opt.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            opt.RoutePrefix = "swagger";
        });

        return app;
    }
}
