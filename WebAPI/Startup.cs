using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repository.DatabaseModels;
using BusinessLogic;

namespace WebAPI
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
            // Register the configured Db context as a service
            services.AddDbContext<Cinephiliacs_DbContext>(options =>
            {
                options.UseSqlServer("Server=tcp:cinephiliacs.database.windows.net,1433;Initial Catalog=Cinephiliacs_Db;Persist Security Info=False;User ID=kugelsicher;Password=F36UWevqvcDxEmt;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            });

            // Register all business logic classes as services
            //services.AddScoped<StoreMethods>();
            services.AddScoped<UserMethods>();

            // Register the repository class as a service
            //services.AddScoped<TheStoreRepo>();

            // Register the WebAPI controllers as services
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }
            // Enables default text-only handlers for common error status codes
            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            // Redirects unknown paths to the index HTML
            app.UseRewriter(new RewriteOptions()
                .AddRedirect("^$", "index.html"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
