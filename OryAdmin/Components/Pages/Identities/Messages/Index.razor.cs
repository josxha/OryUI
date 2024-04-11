using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Messages;

public partial class Index : ComponentBase
{
    private bool _isLoading = true;
    private List<KratosMessage>? _messages;

    [SupplyParameterFromQuery(Name = "page_token")]
    private string? PageToken { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        // TODO use pagination to support a large amount
        _messages = await ApiService.KratosCourier.ListCourierMessagesAsync();

        _isLoading = false;
    }
}