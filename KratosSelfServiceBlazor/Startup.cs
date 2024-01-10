using System.Globalization;
using System.Security.Claims;
using KratosSelfServiceBlazor.Components;
using KratosSelfServiceBlazor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;

namespace KratosSelfServiceBlazor;

public class Startup(IConfigurationRoot config, IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents();
        services.AddHttpContextAccessor();

        // localisation
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddSingleton<ICustomTranslator, CustomTranslator>();
        services.AddSingleton<IOryElementsTranslator, OryElementsTranslator>();

        // own services
        services.AddSingleton<EnvService>();
        services.AddSingleton<ApiService>();
        services.AddSingleton<IdentitySchemaService>();
    }

    public void Configure(WebApplication app)
    {
        // localisation
        var supportedCultures = new[]
        {
            new CultureInfo("en"),
            new CultureInfo("de"),
            new CultureInfo("es"),
            new CultureInfo("fr"),
            new CultureInfo("nl"),
            new CultureInfo("pt"),
            new CultureInfo("se")
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
            app.UseExceptionHandler("/error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this
            // for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseAntiforgery();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
    }
}