using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Repos;
using PartyRaidR.Backend.Repos.Promises;

namespace PartyRaidR.Backend.Extensions
{
    public static class BackendExtension
    {
        public static void ConfigureServer(this IServiceCollection services, string? connectionString)
        {
            services.ConfigureOpenApi();
            services.ConfigureCors();
            services.AddAppDbContext(connectionString);
            services.ConfigureRepositories();
        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy(name: "PartyRaidRCors", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                }
              )
            );
        }

        private static void ConfigureOpenApi(this IServiceCollection services)
        {
            services.AddOpenApi();
            services.AddSwaggerGen();
        }

        private static void AddAppDbContext(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure()
                )
            );
        }

        private static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IApplicationRepo, ApplicationRepo>();
            services.AddScoped<ICityRepo, CityRepo>();
            services.AddScoped<IEventRepo, EventRepo>();
            services.AddScoped<INotificationRepo, NotificationRepo>();
            services.AddScoped<IPlaceRepo, PlaceRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
        }
    }
}
