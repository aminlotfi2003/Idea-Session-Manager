using Asp.Versioning.ApiExplorer;
using ISM.Domain.Identity;
using ISM.Infrastructure.Persistence.Contexts;
using ISM.WebFramework.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ISM.WebFramework.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseWebFrameworkPipeline(this WebApplication app)
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwaggerConfigured(provider);

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        await ApplicationDbContextSeed.SeedAdminUserAsync(userManager, roleManager);
    }
}
