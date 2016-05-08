using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Microsoft.Data.Entity;
using CMAP.Models;

namespace CMAP
{
    public class Startup
    {
        // Startup Constructor for Configuration
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables()
            ;

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Cors support to the service
            var policy = new Microsoft.AspNet.Cors.Infrastructure.CorsPolicy();
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            policy.SupportsCredentials = true;
            services.AddCors(configure => configure.AddPolicy("AllowAll", policy));

            // Add MVC
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = 
                        new CamelCasePropertyNamesContractResolver();
                        // Reference Loop Handling
                        options.SerializerSettings.ReferenceLoopHandling 
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                    })
                    ;

            // Swagger
            services.AddSwaggerGen();

            // Entity Framework w/ SQL Server
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            // Add Logger Outputs
            loggerFactory.AddDebug();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            // iis
            app.UseIISPlatformHandler();

            // Developer Pages
            if( env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseRuntimeInfoPage();
            }

            // Serve Static Files
            app.UseDefaultFiles("/client");
            app.UseStaticFiles();

            // CORS
            app.UseCors("AllowAll");

            // Swagge
            app.UseSwaggerGen();
            app.UseSwaggerUi();

            // MVC
            app.UseMvcWithDefaultRoute();

            // Terminal Middleware
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
