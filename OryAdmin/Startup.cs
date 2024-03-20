using System.Globalization;
using Microsoft.AspNetCore.Localization;
using OryAdmin.Components;
using OryAdmin.Services;

namespace OryAdmin;

public class Startup(IConfigurationRoot config, IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(typeof(ILogger), provider => provider.GetRequiredService<ILogger<Program>>());
        services.AddHttpContextAccessor();
        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // localisation
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddSingleton<ICustomTranslator, CustomTranslator>();

        // own services
        services.AddSingleton<EnvService>();
        services.AddSingleton<ApiService>();
        services.AddSingleton<IdentitySchemaService>();
    }

    public Task Configure(WebApplication app)
    {
        // localisation
        var supportedCultures = new[]
        {
            new CultureInfo("en"),
            new CultureInfo("de")
        };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("de"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios,
            // see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseAntiforgery();
        
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        return Task.CompletedTask;
    }
}