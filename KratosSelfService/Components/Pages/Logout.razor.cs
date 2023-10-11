using KratosSelfService.Services;
using Microsoft.AspNetCore.Components;

namespace KratosSelfService.Components.Pages;

public partial class Logout
{
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var headers = HttpContextAccessor.HttpContext?.Request.GetTypedHeaders();
        var cookies = headers?.Cookie;
        var cookie = cookies?.FirstOrDefault(cookie => cookie.Name == "ory_kratos_session")?.Value;
        var token = cookie?.ToString();
        var flow = await ApiService.FrontendApi.CreateBrowserLogoutFlowAsync(token);
        Navigation.NavigateTo($"{flow.LogoutUrl}?token={flow.LogoutToken}");
    }
}