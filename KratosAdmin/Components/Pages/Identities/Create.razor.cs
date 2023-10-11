using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;

namespace KratosAdmin.Components.Pages.Identities;

public partial class Create
{
    private bool _isLoading = true;
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _isLoading = false;
    }
}