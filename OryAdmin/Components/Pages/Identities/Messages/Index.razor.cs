using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Messages;

public partial class Index : ComponentBase
{
    private bool _isLoading = true;
    private List<KratosMessage>? _messages;

    [SupplyParameterFromQuery(Name = "page")]
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    private int PageNr { get; set; } = 1;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        // TODO use pagination to support a large amount
        _messages = await ApiService.KratosCourier.ListCourierMessagesAsync();

        _isLoading = false;
    }
}