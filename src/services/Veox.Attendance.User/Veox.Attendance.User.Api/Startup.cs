using System;
using System.Text;
using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Veox.Attendance.User.Api.Attributes;
using Veox.Attendance.User.Api.Extensions;
using Veox.Attendance.User.Infraestructure.MongoDb;
using Veox.Attendance.User.IoC;

// ReSharper disable ConvertToUsingDeclaration

namespace Veox.Attendance.User.Api
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
            services.AddCors();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(GenerateSessionAttribute));
                options.Filters.Add(typeof(ErrorHandlerAttribute));
                options.EnableEndpointRouting = false;
            });

            services.AddControllers();

            services.Configure<MongoDbOptions>(Configuration.GetSection("MongoDb"));

            var securityKey = Encoding.ASCII.GetBytes(Configuration["Jwt:SecretId"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerConfiguration();

            services.AddApiVersioningExtension();

            services.AddHealthChecks();

            services.AddSingleton<IConsulClient>(p => new ConsulClient(o =>
            {
                var hostName = Configuration["Consul:ConsulUri"];

                if (!string.IsNullOrEmpty(hostName))
                {
                    o.Address = new Uri(hostName);
                }
            }));

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseHealthChecks("/health");

            ConfigureConsul(app);
        }

        private void ConfigureConsul(IApplicationBuilder app)
        {
            var serviceName = Configuration["Consul:ServiceName"];
            var serviceUri = Configuration["Consul:ServiceUri"];

            var service = new Uri(serviceUri);
            var client = app.ApplicationServices.GetRequiredService<IConsulClient>();

            var serviceId = Guid.NewGuid();
            var consulServiceId = $"{serviceName}:{serviceId}";

            var serviceRegistration = new AgentServiceRegistration
            {
                ID = consulServiceId,
                Address = service.Host,
                Port = service.Port,
                Name = serviceName,
                Check = new AgentCheckRegistration
                {
                    HTTP = $"{serviceUri}/health",
                    Notes = $"Checks {serviceUri}/health on {service.Host}:[{service.Port}]",
                    Timeout = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(30),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(60)
                }
            };

            client.Agent.ServiceRegister(serviceRegistration).GetAwaiter().GetResult();
        }
    }
}