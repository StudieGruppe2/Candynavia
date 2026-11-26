using Backend.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Dependency Injection: .NET får besked på, hvilket repository
// den skal give til controllerne, når de beder om et interface

/*
// Memory-repositories
builder.Services.AddSingleton<IPostalCodeRepository, PostalCodeRepositoryMemory>();
builder.Services.AddSingleton<IMeetingRequestRepository, MeetingRequestRepositoryMemory>();
builder.Services.AddSingleton<ISalesPersonRepository, SalesPersonRepositoryMemory>();
builder.Services.AddSingleton<ITimeSlotRepository, TimeSlotRepositoryMemory>();
*/

//MongoDB-repositories
builder.Services.AddSingleton<ISalesPersonRepository, SalesPersonRepositoryMongoDB>();
builder.Services.AddSingleton<IPostalCodeRepository, PostalCodeRepositoryMongoDB>();
builder.Services.AddSingleton<ITimeSlotRepository, TimeSlotRepositoryMongoDB>();
builder.Services.AddSingleton<IMeetingRequestRepository, MeetingRequestRepositoryMongoDB>();


// Giver frontend lov til at kalde backend
builder.Services.AddCors(OpsaetCors);

var app = builder.Build();


// Skal stå før MapControllers
app.UseCors();

// Skal anvendes ved senere implementering af adgangskontrol for sælgere
app.UseAuthorization();

app.MapControllers();

app.Run();

// Tillader frontend at sende HTTP-requests til backend
static void OpsaetCors(CorsOptions options)
{
    CorsPolicy policy = new CorsPolicyBuilder()
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .Build();

    options.AddDefaultPolicy(policy);
}