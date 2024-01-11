using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Client;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Logout
{
    [SupplyParameterFromQuery(Name = "challenge")]
    private string? logoutChallenge { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // show a dialog to let the user confirm that he wants to log out
        if (!string.IsNullOrWhiteSpace(logoutChallenge)) return;

        // end kratos session
        try
        {
            var flow = await api.Frontend.CreateBrowserLogoutFlowAsync(accessor.HttpContext!.Request.Headers.Cookie);
            nav.NavigateTo(flow.LogoutUrl);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Could not get logout flow: {Message}", exception.Message);
            nav.NavigateTo("/");
        }
    }
}