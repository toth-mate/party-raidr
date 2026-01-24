using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Context;

namespace PartyRaidR.Backend.Extensions
{
    public static class WebApplicationExtension
    {
        public static void ConfigureWebApp(this WebApplication app)
        {
            app.ConfigureDatabase();

            if (app.Environment.IsDevelopment())
                app.ConfigureOpenApi();

            app.UseHttpsRedirection();

            app.ConfigureWebAppCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }

        private static void ConfigureWebAppCors(this WebApplication app)
        {
            app.UseCors("PartyRaidRCors");
        }

        private static void ConfigureOpenApi(this WebApplication app)
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        private static void ConfigureDatabase(this WebApplication app)
        {
            // Migration upon start
            using (var scope = app.Services.CreateScope())
            {
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
