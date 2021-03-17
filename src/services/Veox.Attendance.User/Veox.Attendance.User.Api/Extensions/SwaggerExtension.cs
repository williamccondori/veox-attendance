using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Veox.Attendance.User.Api.Extensions
{
    /// <summary>
    /// Configure swagger's options. 
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Configure swagger's options. 
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }

            var applicationName = Assembly.GetExecutingAssembly().GetName().Name;

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = applicationName,
                    Description = applicationName + " app swagger documentation."
                });

                var xmlFile = $"{applicationName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);
            });
        }

        /// <summary>
        /// Adds a swagger middleware to application.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "v1");
                c.RoutePrefix = "swagger";
                c.DefaultModelsExpandDepth(-1);
            });
        }

        /// <summary>
        /// Configure the API versioning extension.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}