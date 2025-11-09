using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Context;

namespace PartyRaidR.Backend.Extensions
{
    public static class BackendExtension
    {
        public static void ConfigureServer(this IServiceCollection services, string? connectionString)
        {
            services.ConfigureOpenApi();
            services.ConfigureCors();
            services.AddAppDbContext(connectionString);
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
    }
}
