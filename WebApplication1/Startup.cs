using ClinicAppointmentSystem.BOL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ClinicAppointmentSystemAPI.BLL;
using ClinicAppointmentSystemAPI.DAL;

namespace ClinicAppointmentSystemAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure DbContext
            services.AddDbContext<ClinicAppointmentContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
           
            services.AddScoped<DoctorBS>();
            services.AddScoped<DoctorDB>();
            
            services.AddScoped<PatientBS>();
            services.AddScoped<PatientDB>();
            
            services.AddScoped<AppointmentBS>();
            services.AddScoped<AppointmentDB>();

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ClinicAppointment API",
                    Version = "v1"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction()) // Make Swagger available always
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClinicAppointment API V1");
                    c.RoutePrefix = string.Empty; // Serve Swagger at root URL
                });
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
