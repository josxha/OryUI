using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Users;

public partial class Sessions
{
    private List<KratosSession>? _activeSessions;
    private List<KratosSession>? _inactiveSessions;
    private bool _isLoading = true;
    private KratosSession? _sessionToExtend;
    private KratosSession? _sessionToInvoke;
    private bool _showDeleteSessionsModal;
    [Parameter] public string? UserId { get; set; }
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ReloadSessions();
        _isLoading = false;
    }

    private async Task ReloadSessions()
    {
        var tasks = new List<Task>
        {
            Task.Run(async () =>
            {
                _activeSessions = await ApiService.KratosIdentity.ListIdentitySessionsAsync(UserId, active: true);
            }),
            Task.Run(async () =>
            {
                _inactiveSessions =
                    await ApiService.KratosIdentity.ListIdentitySessionsAsync(UserId, active: false);
            })
        };

        await Task.WhenAll(tasks);
    }

    private async Task DeleteIdentitySessions()
    {
        await ApiService.KratosIdentity.DeleteIdentitySessionsAsync(UserId);
        _showDeleteSessionsModal = false;
        await ReloadSessions();
    }

    private async Task InvokeSession(string sessionId)
    {
        await ApiService.KratosIdentity.DisableSessionAsync(sessionId);
        _sessionToInvoke = null;
        await ReloadSessions();
    }

    private async Task ExtendSession(string sessionId)
    {
        await ApiService.KratosIdentity.ExtendSessionAsync(sessionId);
        _sessionToExtend = null;
        await ReloadSessions();
    }
}