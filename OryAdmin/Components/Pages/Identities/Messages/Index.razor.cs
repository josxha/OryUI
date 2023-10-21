using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Messages;

public partial class Index : ComponentBase
{
    private bool _isLoading = true;
    private List<KratosMessage> _messages;

    [SupplyParameterFromQuery(Name = "page")]
    private int? PageNr { get; set; }

    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        PageNr ??= 1;

        // TODO use pagination to support a large amount
        _messages = await ApiService.KratosCourier.ListCourierMessagesAsync();

        _isLoading = false;
    }

    private void RefreshPage(MouseEventArgs arg)
    {
        nav.Refresh(true);
    }
}