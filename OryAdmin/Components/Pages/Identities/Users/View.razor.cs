using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Ory.Hydra.Client.Model;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Users;

public partial class View
{
    private List<KratosSession>? _activeSessions;
    private KratosIdentity? _identity;
    private bool _isLoading = true;
    private List<HydraOAuth2ConsentSession>? _oauth2Sessions;
    private HydraOAuth2ConsentSession? _oauth2SessionToShowDetailsFor;
    private KratosSession? _sessionToInvoke;
    private bool _showDeleteIdentityModal;
    private bool _showRevokeOAuth2SessionsModal;
    [Parameter] public string UserId { get; set; } = default!;
    [Inject] private ApiService ApiService { get; set; } = default!;
    [Inject] private EnvService EnvService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var tasks = new List<Task>
        {
            Task.Run(async () => { _identity = await ApiService.KratosIdentity.GetIdentityAsync(UserId); }),
            Task.Run(async () =>
            {
                _activeSessions = await ApiService.KratosIdentity.ListIdentitySessionsAsync(UserId, active: true);
            })
        };

        if (EnvService.EnabledHydra)
            tasks.Add(Task.Run(async () =>
            {
                _oauth2Sessions = await ApiService.HydraOAuth2.ListOAuth2ConsentSessionsAsync(UserId);
            }));

        await Task.WhenAll(tasks);
        _isLoading = false;
    }

    private async Task DeleteIdentity()
    {
        await ApiService.KratosIdentity.DeleteIdentityAsync(UserId);
        nav.NavigateTo("identities/users");
    }

    private async Task UpdatePassword()
    {
        var body = new KratosCreateRecoveryLinkForIdentityBody(identityId: UserId);
        var link = await ApiService.KratosIdentity
            .CreateRecoveryLinkForIdentityAsync(null, body);
        nav.NavigateTo(link.RecoveryLink);
    }

    private async Task RevokeOAuth2ConsentSessions()
    {
        await ApiService.HydraOAuth2.RevokeOAuth2ConsentSessionsAsync(UserId, all: true);
        _showRevokeOAuth2SessionsModal = false;
        _oauth2Sessions = await ApiService.HydraOAuth2.ListOAuth2ConsentSessionsAsync(UserId);
    }

    private async Task DisableIdentitySession(string sessionId)
    {
        await ApiService.KratosIdentity.DisableSessionAsync(sessionId);
        _activeSessions = await ApiService.KratosIdentity.ListIdentitySessionsAsync(UserId, active: true);
    }
}