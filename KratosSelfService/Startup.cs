using System.Globalization;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Localization;

namespace KratosSelfService;

public class Startup(IConfigurationRoot config, IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllersWithViews();

        // localisation
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddSingleton<ICustomTranslator, CustomTranslator>();
        services.AddSingleton<IOryElementsTranslator, OryElementsTranslator>();

        // own services
        services.AddSingleton<EnvService>();
        services.AddSingleton<ApiService>();
    }

    public void Configure(WebApplication app)
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
            // The default HSTS value is 30 days. You may want to change this
            // for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
    }
}