using System.Globalization;
using System.Security.Claims;
using KratosSelfServiceBlazor.Components;
using KratosSelfServiceBlazor.Services;
using KratosSelfServiceBlazor.Utils;
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

        services.AddControllersWithViews();

        // authentication and authorisation
        services.AddAuthentication(options =>
        {
            options.AddScheme<AuthenticationHandler>("DefaultScheme", "Default authentication scheme");
            options.DefaultChallengeScheme = "DefaultScheme"; // 401 Unauthorized
            options.DefaultForbidScheme = "DefaultScheme"; // 403 Forbid
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("LoggedIn", policyBuilder => { policyBuilder.RequireClaim(ClaimTypes.NameIdentifier); });
            // The fallback authorization policy requires all users to be authenticated, except for controllers or action
            // methods with an authorization attribute. For example, controllers or action methods with [AllowAnonymous] or
            // [Authorize(PolicyName="MyPolicy")] use the applied authorization attribute rather than the fallback
            // authorization policy.
            // The fallback authorization policy is applied to all requests that don't explicitly specify an authorization
            // policy.
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        
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
            DefaultRequestCulture = new RequestCulture("en"),
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
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
    }
}