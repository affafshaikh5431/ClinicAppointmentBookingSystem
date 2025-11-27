using System;
using ClinicAppointmentSystem.BOL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClinicAppointmentSystemAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ClinicAppointmentContext>();
                try
                {
                    if (db.Database.CanConnect())
                    {
                        Console.WriteLine("Database connection successful!");
                    }
                    else
                    {
                        Console.WriteLine("Database connection failed!");
                    }

                    db.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to database: {ex.Message}");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
