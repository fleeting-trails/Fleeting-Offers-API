using System.Diagnostics;
using FleetingOffers.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Initialize database

#region Initializations
new DatabaseSettings(builder);
new GeneralSettings(builder);
new MailSettings(builder);
new CacheSettings(builder);
new AutoMapperSettings(builder);
new JWTSettings(builder);
#endregion

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    foreach (var endpoint in endpoints.DataSources.SelectMany(ds => ds.Endpoints))
    {
        Console.WriteLine($"Available Endpoint: {endpoint.DisplayName}");
        Debug.WriteLine($"Available Endpoint: {endpoint.DisplayName}");
    }
});

app.MapControllers();

// Log all registered routes
var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();
foreach (var endpoint in endpointDataSource.Endpoints)
{
    Console.WriteLine($"Available URL: {endpoint.DisplayName}");
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/test", () =>
{
    return "Connection OK!";
})
.WithName("TestConnection");

app.Run();
