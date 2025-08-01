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

builder.Services.AddScoped<INoteHttpService, NoteHttpService>();
builder.Services.AddScoped<IItemHttpService, ItemHttpService>();
builder.Services.AddScoped<IPersonHttpService, PersonHttpService>();
builder.Services.AddScoped<IPlaceHttpService, PlaceHttpService>();
builder.Services.AddScoped<NoteAssignmentService>();




await builder.Build().RunAsync();
