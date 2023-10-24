using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities;

public partial class Index
{
    private readonly DateTime _now = DateTime.UtcNow;
    private Dictionary<DateOnly, int>? _dailySignUps;
    private List<KratosIdentity>? _identities;
    private bool _isLoading = true;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _identities = await ApiService.KratosIdentity.ListIdentitiesAsync();
        var fourWeeksAgo = _now - new TimeSpan(28, 0, 0, 0);
        _dailySignUps = _identities
            .Where(identity => identity.CreatedAt > fourWeeksAgo)
            .GroupBy(identity => DateOnly.FromDateTime(identity.CreatedAt))
            .ToDictionary(group => group.Key, group => group.Count());
        _isLoading = false;
    }
}