using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Gør det muligt at bruge Swagger til at teste API'et
//builder.Services.AddSwaggerGen();

// Dependency Injection: .NET får besked på, hvilket repository
// den skal give til controllerne, når de beder om et interface

// Memory-repositories (bruges ikke lige nu)
builder.Services.AddSingleton<IPostalCodeRepository, PostalCodeRepositoryMemory>();
builder.Services.AddSingleton<IMeetingRequestRepository, MeetingRequestRepositoryMemory>();
builder.Services.AddSingleton<ISalesPersonRepository, SalesPersonRepositoryMemory>();
builder.Services.AddSingleton<ITimeSlotRepository, TimeSlotRepositoryMemory>();

/*
// MongoDB-repositories (bruges lige nu)
builder.Services.AddSingleton<ISalesPersonRepository, SalesPersonRepositoryMongoDB>();
builder.Services.AddSingleton<IPostalCodeRepository, PostalCodeRepositoryMongoDB>();
builder.Services.AddSingleton<ITimeSlotRepository, TimeSlotRepositoryMongoDB>();
builder.Services.AddSingleton<IMeetingRequestRepository, MeetingRequestRepositoryMongoDB>();
*/

// Giver frontend lov til at kalde backend
builder.Services.AddCors(OpsaetCors);

var app = builder.Build();

// Swagger aktiveres, når backend kører i Development


// Skal stå før MapControllers
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();


// ---------- CORS ----------

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