using Almostengr.ThermometerPi.Api.Clients;
using Almostengr.ThermometerPi.Api.Database;
using Almostengr.ThermometerPi.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Almostengr.ThermometerPi.Api.Workers;

namespace Almostengr.ThermometerPi.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Almostengr.ThermometerPi.Api", Version = "v1" });
            });

            services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase("ThermometerPi"));
            services.AddScoped<ITemperatureRepository, TemperatureRepository>();
            services.AddScoped<ITemperatureReadingService, TemperatureReadingService>();

            # if RELEASE
                services.AddScoped<ISensorService, Ds18b20Service>();
                services.AddScoped<ILcdService, LcdService>();
                services.AddScoped<INwsClient, NwsClient>();
                services.AddHostedService<LcdDisplayWorker>();
            # else
                services.AddScoped<ISensorService, MockSensorService>();
                services.AddScoped<ILcdService, MockLcdService>();
                // services.AddScoped<INwsClient, NwsClient>();
                services.AddScoped<INwsClient, MockNwsClient>();
            # endif

            services.AddHostedService<InteriorLatestWorker>();
            // services.AddHostedService<NwsLatestWorker>();
            services.AddHostedService<DbMaintenanceWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Almostengr.ThermometerPi.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
