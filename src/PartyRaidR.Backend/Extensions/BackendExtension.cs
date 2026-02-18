using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PartyRaidR.Backend.Assemblers;
using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Repos;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services;
using PartyRaidR.Backend.Services.Promises;

namespace PartyRaidR.Backend.Extensions
{
    public static class BackendExtension
    {
        public static void ConfigureServer(this IServiceCollection services, string? connectionString)
        {
            services.AddControllers();
            services.ConfigureApiEndpoints();
            services.ConfigureOpenApi();
            services.ConfigureCors();
            services.AddAppDbContext(connectionString);
            services.ConfigureRepositories();
            services.ConfigureServices();
            services.ConfigureAssemblers();

            // Service can access the HTTP request
            services.AddHttpContextAccessor();
        }

        private static void ConfigureApiEndpoints(this IServiceCollection services)
        {
            // Following conventions by making automatically generated API endpoints lowercase
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "PartyRaidR API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Token required in the following pattern: Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        private static void AddAppDbContext(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure()
                                                .UseNetTopologySuite()
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

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserContext, UserContext>();

            // Model related services
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEventService, EventService>();
        }

        private static void ConfigureAssemblers(this IServiceCollection services)
        {
            services.AddScoped<ApplicationAssembler>();
            services.AddScoped<CityAssembler>();
            services.AddScoped<EventAssembler>();
            services.AddScoped<NotificationAssembler>();
            services.AddScoped<PlaceAssembler>();
            services.AddScoped<UserAssembler>();
            services.AddScoped<UserRegistrationAssembler>();
        }
    }
}
