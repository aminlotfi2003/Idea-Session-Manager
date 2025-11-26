using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ISM.WebFramework.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfigured(this IServiceCollection services)
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
