using TesteMigrations.Infrastructure.Identity;
using TesteMigrations.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using FirebirdSql.EntityFrameworkCore.Firebird.Extensions;
using Microsoft.Extensions.Configuration;

namespace TesteMigrations.WebUI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var configuration = scope.ServiceProvider.GetService<IConfiguration>();

                    if(configuration["provider"] == "sqlserver")
                    {
                        var context = services.GetRequiredService<ApplicationDbContext>();

                        var enableMigrations = context.Database.IsSqlServer() ||
                            context.Database.IsSqlite() ||
                            context.Database.IsFirebird() ||
                            context.Database.IsOracle();

                        if (enableMigrations)
                        {
                            context.Database.Migrate();
                        }

                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                        await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager);
                        await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                    }
                    else if(configuration["provider"] == "sqlite")
                    {
                        var context = services.GetRequiredService<ApplicationSqliteDbContext>();

                        var enableMigrations = context.Database.IsSqlServer() ||
                            context.Database.IsSqlite() ||
                            context.Database.IsFirebird() ||
                            context.Database.IsOracle();

                        if (enableMigrations)
                        {
                            context.Database.Migrate();
                        }

                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                        await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager);
                        await ApplicationDbContextSeed.SeedSampleDataAsync(context);

                    }
                    
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
