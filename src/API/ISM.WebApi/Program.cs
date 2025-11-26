using Asp.Versioning.ApiExplorer;
using ISM.WebFramework.Extensions;
using ISM.WebFramework.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfigured(provider);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
