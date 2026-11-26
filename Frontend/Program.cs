using Frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args); //Opretter og konfigurerer Blazor WebAssembly-applikationen

builder.RootComponents.Add<App>("#app");//Tilknytter hovedkomponenten App til index.html

builder.RootComponents.Add<HeadOutlet>("head::after");//Gør det muligt at ændre eksempelvis sidens titel

//Henter backendens adresse fra wwwroot/appsettings.json
//Hvis adressen ikke findes, anvendes standardadressen
var backendUrl = builder.Configuration["BackendUrl"] ?? "http://localhost:5002/";

//Registrerer HttpClient gennem Dependency Injection,
//så frontendens komponenter kan sende HTTP-requests til backend
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(backendUrl) //Angiver backendens grundadresse
});

await builder.Build().RunAsync();//Bygger og starter frontendapplikationen
