using Microsoft.EntityFrameworkCore;
using Random.User.Rest.Infrastructure;
using Random.User.Rest.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("UserList"));
builder.Services.AddOptions<ConfigurationOptions>()
    .Bind(builder.Configuration.GetSection("ConfigurationOptions"))
    .Validate(options => options.MaxUsersLimit is > 0 and <= 1000, "MaxUsersLimit must be between 1 and 1000.")
    .Validate(options => Uri.TryCreate(options.GeneratorSiteURI, UriKind.Absolute, out var uri) && uri.Scheme == Uri.UriSchemeHttps,
        "GeneratorSiteURI must be an absolute HTTPS URL.")
    .ValidateOnStart();
builder.Services.AddHttpClient<IdentityInitializer>(client => client.Timeout = TimeSpan.FromSeconds(15));

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}
app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks("/health");

// A failed upstream request must not prevent the local CRUD API from starting.
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Seed");
    try
    {
        await scope.ServiceProvider.GetRequiredService<IdentityInitializer>().SeedAsync();
    }
    catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or System.Text.Json.JsonException)
    {
        logger.LogWarning(ex, "Sample data unavailable; starting with an empty database.");
    }
}
await app.RunAsync();
