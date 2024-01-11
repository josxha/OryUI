using KratosSelfServiceBlazor.Extensions;
using Ory.Kratos.Client.Model;
using UAParser;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Sessions
{
    private bool _isLoading = true;
    private KratosSession _currentSession = default!;
    private List<KratosSession> _otherSessions = default!;
    private int _totalSessions;
    private Parser _uaParser = Parser.GetDefault();

    protected override async Task OnInitializedAsync()
    {
        _currentSession = accessor.HttpContext!.GetSession()!;
        // retrieve all other active sessions
        _otherSessions = await api.Frontend
            .ListMySessionsAsync(cookie: accessor.HttpContext!.Request.Headers.Cookie) ?? [];
        _totalSessions = _otherSessions.Count + 1;
        _isLoading = false;
    }
}