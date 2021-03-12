using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Veox.Attendance.Record.Api.Extensions;
using Veox.Attendance.Record.Infraestructure.MongoDb;
using Veox.Attendance.Record.IoC;

namespace Veox.Attendance.Record.Api
{
    /// <summary>
    /// Init the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Init the application.
        /// </summary>
        /// <param name="configuration">App's configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbOptions>(Configuration.GetSection("MongoDb"));

            services.AddControllers();

            services.AddSwaggerConfiguration();

            services.AddApiVersioningExtension();

            services.AddHealthChecks();

            services.AddServiceDependency();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Web host environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerSetup();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseErrorHandler();

            app.UseHealthChecks("/health");

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}