using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi; // Add this using directive
using Swashbuckle.AspNetCore.SwaggerGen; // Add this using directive
using Swashbuckle.AspNetCore.SwaggerUI; // Add this using directive
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace Ordering.API
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddApiServices(
         this IServiceCollection services , IConfiguration configuration)
        {


            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddCarter();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();

            services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("DataBase")!);
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.UseExceptionHandler(op => { });

            app.MapCarter();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();


                app.MapGet("/", () => Results.Redirect("/swagger"));

            }

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}