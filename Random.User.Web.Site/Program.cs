var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("RandomUserApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ConfigurationOptions:ApiURI"]
        ?? throw new InvalidOperationException("ConfigurationOptions:ApiURI is required."));
    client.Timeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
await app.RunAsync();
