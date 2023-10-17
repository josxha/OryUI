using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Messages;

public partial class View : ComponentBase
{
    private bool _isLoading = true;
    private KratosMessage? _message;
    [Parameter] public string? MessageId { get; set; }
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _message = await ApiService.KratosCourier.GetCourierMessageAsync(MessageId);
        _isLoading = false;
    }
}