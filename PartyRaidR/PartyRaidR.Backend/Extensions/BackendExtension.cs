namespace PartyRaidR.Backend.Extensions
{
    public static class BackendExtension
    {
        public static void ConfigureServer(this IServiceCollection services)
        {
            services.ConfigureOpenApi();
            services.ConfigureCors();
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
    }
}
