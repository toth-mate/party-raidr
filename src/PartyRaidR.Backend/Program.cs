using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.ConfigureServer(connectionString);


var app = builder.Build();


// Migration upon start
using (var scope = app.Services.CreateScope())
{
    AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PartyRaidRCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
