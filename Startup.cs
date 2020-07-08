using System;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using HelloApi.Models;
using HelloApi.Services;

namespace HelloApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen();

            services.AddDbContext<HelloContext>(options =>
              options.UseSqlServer("Server=localhost; Database=hello-db; User=sa; Password=P@ssw0rd;"));
            services.AddScoped<IFileAccessService, FileAccessService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);
            SeedInitialData(app);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hello API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var dataContext = serviceScope.ServiceProvider.GetService<HelloContext>();
            dataContext.Migrate();
        }

        // Seed Initial Data
        private static void SeedInitialData(IApplicationBuilder app)
        {            
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var fileAccess = serviceScope.ServiceProvider.GetService<IFileAccessService>();
            fileAccess.InitialData();
        }
    }
}