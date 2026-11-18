# Random User API

An educational **ASP.NET Core 10** REST API that imports sample profiles from [RandomUser.me](https://randomuser.me/), stores them in an **EF Core in-memory database**, and exposes CRUD endpoints. A separate MVC website displays the first page of results.

The project demonstrates controllers, dependency injection, typed `HttpClient`, configuration validation, EF Core relationships, asynchronous I/O, pagination, error handling and automated tests.

> **Demo only:** data is held in memory and disappears when the API restarts. This is not a production identity service; sample profiles must not be used for authentication.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Internet access at startup to import sample profiles (optional: the API starts empty if the upstream service is unavailable).

## Run

From the repository root, open two terminals:

```bash
dotnet restore Random.User.Rest.sln
dotnet run --project Random.User.Rest --launch-profile Random.User.Rest
```

```bash
dotnet run --project Random.User.Web.Site --launch-profile Random.User.Web.Site
```

- API: `http://localhost:5000/api/user`
- MVC demo: `http://localhost:5100`
- Health endpoint: `http://localhost:5000/health`

The API runs independently of the website. Both projects use HTTP for simple local development; configure HTTPS for deployment.

## Endpoints

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/user?page=1&cant=20` | Sorted users, one-based pagination |
| GET | `/api/user/{id}` | User and location by ID (`404` if missing) |
| POST | `/api/user` | Add user (`201`, with `Location` header) |
| PUT | `/api/user/{id}` | Update user (`204`, `404` if missing) |
| DELETE | `/api/user/{id}` | Delete user (`204`, `404` if missing) |
| GET | `/health` | Liveness check |

`page` defaults to `1`; `cant` defaults to `MaxUsersLimit` and is capped at that value. Both must be positive. The `isTheEldest` response flag marks the oldest user **on the returned page** (it is derived rather than persisted).

Example:

```bash
curl 'http://localhost:5000/api/user?page=2&cant=10'
curl 'http://localhost:5000/api/user/1'
curl -X POST 'http://localhost:5000/api/user' \
  -H 'Content-Type: application/json' \
  -d '{"gender":"female","name":"Jane Doe","email":"jane@example.com","birthDate":"1994-01-01T00:00:00Z","uuid":"demo-1","userName":"jane","age":32,"location":{"street":"Main Street 1","city":"Buenos Aires","state":"Buenos Aires","postCode":"1000"}}'
```

PUT requires `userId` in the body matching the route. Omit `location` or send `null` to remove it. POST ignores supplied IDs. Name, username and email are validated.

## Configuration

In `Random.User.Rest/appsettings.json`:

```json
"ConfigurationOptions": {
  "MaxUsersLimit": 50,
  "GeneratorSiteURI": "https://randomuser.me/api/?results=500"
}
```

Override these via environment variables `ConfigurationOptions__MaxUsersLimit` and `ConfigurationOptions__GeneratorSiteURI`. Generator URL must use HTTPS.

The MVC API base URL is `ConfigurationOptions:ApiURI` in `Random.User.Web.Site/appsettings.json` (default `http://localhost:5000/api/`); override via `ConfigurationOptions__ApiURI`.

## Tests

```bash
dotnet test Random.User.Rest.sln
```

The test project covers sorting, pagination, invalid page numbers, and missing-resource CRUD behavior. GitHub Actions builds and tests the solution on pushes and pull requests.

## Scope / future exercises

- The MVC website is a **read-only viewer**; CRUD is available through HTTP. A proper MVC edit form would need server-side validation and anti-forgery protection.
- There is no durable storage or authentication. SQLite persistence is a reasonable next exercise.
- Upstream import happens at startup only; restarting the API regenerates its in-memory data.
- The original `/api/user` routes are retained for compatibility.
