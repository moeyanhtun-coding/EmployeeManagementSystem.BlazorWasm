using Blazored.Toast;
using EmployeeManagementSystem.Wasm;
using EmployeeManagementSystem.Wasm.Authentication;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7230") });
builder.Services.AddScoped<DevCode>();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
