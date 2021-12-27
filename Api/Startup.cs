using Domain.Services;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Notifications;
using Infrastructure.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void InjectDepedencies(IServiceCollection services)
        {
            services.AddScoped<DatabaseContext>();
            
            services.AddScoped<LoginDomain>();
            services.AddScoped<UserDomain>();

            services.AddScoped<HashProvider>();
            services.AddScoped<UserProvider>();
            services.AddScoped<Notification>();

            services.AddTransient<IHashProvider, HashProvider>();
            services.AddTransient<IUserProvider, UserProvider>();
            services.AddTransient<INotification, Notification>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            InjectDepedencies(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
