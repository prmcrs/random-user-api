using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Random.User.Rest.Model;

namespace Random.User.Rest.Infrastructure;

public sealed class IdentityInitializer(HttpClient client, DataContext context, IOptions<ConfigurationOptions> options)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await context.Users.AnyAsync(cancellationToken)) return;

        using var response = await client.GetAsync(options.Value.GeneratorSiteURI, cancellationToken);
        response.EnsureSuccessStatusCode();
        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
        var results = document.RootElement.GetProperty("results");

        foreach (var result in results.EnumerateArray())
        {
            var name = result.GetProperty("name");
            var login = result.GetProperty("login");
            var dob = result.GetProperty("dob");
            var location = result.GetProperty("location");
            var street = location.GetProperty("street");
            // RandomUser.me v1.4 returns street as {number, name}; older versions used a string.
            var streetName = street.ValueKind == JsonValueKind.Object
                ? $"{street.GetProperty("number")} {street.GetProperty("name").GetString()}"
                : street.ToString();
            context.Users.Add(new Model.User
            {
                Gender = result.GetProperty("gender").GetString() ?? "",
                Name = $"{name.GetProperty("title").GetString()} {name.GetProperty("first").GetString()} {name.GetProperty("last").GetString()}",
                Email = result.GetProperty("email").GetString() ?? "",
                BirthDate = dob.GetProperty("date").GetDateTime(),
                Uuid = login.GetProperty("uuid").GetString() ?? "",
                UserName = login.GetProperty("username").GetString() ?? "",
                Age = dob.GetProperty("age").GetInt32(),
                Location = new Model.Location
                {
                    State = location.GetProperty("state").GetString() ?? "",
                    Street = streetName,
                    City = location.GetProperty("city").GetString() ?? "",
                    PostCode = location.GetProperty("postcode").ToString()
                }
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
