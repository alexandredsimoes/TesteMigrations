using TesteMigrations.Application.Common.Interfaces;
using TesteMigrations.Infrastructure.Files;
using TesteMigrations.Infrastructure.Identity;
using TesteMigrations.Infrastructure.Persistence;
using TesteMigrations.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TesteMigrations.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TesteMigrationsDb"));
            }
            else
            {
                var provider = configuration.GetValue<string>("Provider", "sqlserver").ToLowerInvariant();

                if(provider == "sqlserver")
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                                            options.UseSqlServer(
                                                configuration.GetConnectionString("DefaultConnection"),
                                                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

                    services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

                    services.AddDefaultIdentity<ApplicationUser>()
                        .AddEntityFrameworkStores<ApplicationDbContext>();

                    services.AddIdentityServer()
                        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

                }
                else if(provider == "sqlite")
                {
                    services.AddDbContext<ApplicationSqliteDbContext>(options =>
                                            options.UseSqlite(
                                                configuration.GetConnectionString("DefaultConnection"),
                                                b => b.MigrationsAssembly(typeof(ApplicationSqliteDbContext).Assembly.FullName)));

                    services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationSqliteDbContext>());

                    services.AddDefaultIdentity<ApplicationUser>()
                        .AddEntityFrameworkStores<ApplicationSqliteDbContext>();

                    services.AddIdentityServer()
                        .AddApiAuthorization<ApplicationUser, ApplicationSqliteDbContext>();

                }

               

            }

            

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
