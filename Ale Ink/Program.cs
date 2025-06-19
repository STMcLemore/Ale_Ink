using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Ale_Ink;
using Microsoft.EntityFrameworkCore;
using Ale_Ink.HttpServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:7104/") }
);

builder.Services.AddScoped<NoteHttpService>();
builder.Services.AddScoped<ItemHttpService>();
builder.Services.AddScoped<PersonHttpService>();
builder.Services.AddScoped<PlaceHttpService>();

await builder.Build().RunAsync();
