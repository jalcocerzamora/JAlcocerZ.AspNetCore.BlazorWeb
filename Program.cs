using JAlcocerZ.AspNetCore.BlazorWeb.Components;
using JAlcocerZ.AspNetCore.BlazorWeb.Models;

using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

//builder.RootComponents.Add<HeadOutlet>("head::after");

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddHubOptions(options =>
    {
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
        options.HandshakeTimeout = TimeSpan.FromSeconds(30);
    });

// Example of loading a configuration as configuration isn't available yet at this stage.
builder.Services.AddSingleton(provider =>
{
    var config = provider.GetService<IConfiguration>();
    return config?.GetSection("App").Get<AppSettings>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
