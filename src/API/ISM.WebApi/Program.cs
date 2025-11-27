using ISM.WebFramework.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
await app.SeedDatabaseAsync();

app.UseWebFrameworkPipeline();

app.Run();
