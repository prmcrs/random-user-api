using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Random.User.Rest.Infrastructure;
using Random.User.Rest.Model;

namespace Random.User.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Compatibility
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Cookies
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Options from appSettings
            services.Configure<ConfigurationOptions>(Configuration.GetSection("ConfigurationOptions"));
            services.AddScoped(sp => sp.GetService<IOptionsSnapshot<ConfigurationOptions>>().Value);

            // Context
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("UserList"));
        
            // Context Initializer 
            services.AddTransient<IdentityInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IdentityInitializer identityInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Do the initializer
            identityInitializer.Seed().Wait();

            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
