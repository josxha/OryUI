using System.Globalization;
using KratosAdmin.Components;
using KratosAdmin.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Localization;
using Microsoft.Net.Http.Headers;

namespace KratosAdmin;

public class Startup(IConfigurationRoot config, IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.All;
            options.RequestHeaders.Add(HeaderNames.UserAgent);
        });
        services.AddRazorComponents()
            .AddServerComponents();

        // localisation
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddSingleton<ICustomTranslator, CustomTranslator>();

        // own services
        services.AddSingleton<EnvService>();
        services.AddSingleton<ApiService>();
        services.AddSingleton<IdentityService>();
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

        app.MapRazorComponents<App>()
            .AddServerRenderMode();
        return Task.CompletedTask;
    }
}