using BookCatalogFrontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var backendUrl = "http://localhost:60695/api";
if (string.IsNullOrEmpty(backendUrl))
    throw new InvalidOperationException("The Backend URL is not configured.");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendUrl) });

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendUrl) });
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
await builder.Build().RunAsync();
