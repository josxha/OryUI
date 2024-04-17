using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Extensions;
using OryAdmin.Services;
using OryAdmin.Utils;

namespace OryAdmin.Components.Pages.Identities.Messages;

public partial class Index : ComponentBase
{
    private bool _isLoading = true;

    private PaginationTokens PaginationTokens { get; set; } = null!;

    [SupplyParameterFromQuery(Name = "page_size")]
    private int PageSize { get; set; }
    
    private List<KratosMessage>? _messages;

    [SupplyParameterFromQuery(Name = "page_token")]
    private string? PageToken { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        _isLoading = true;
        if (PageSize == 0) PageSize = 50;
        
        var messagesResponse = await ApiService.KratosCourier
            .ListCourierMessagesWithHttpInfoAsync(pageSize:PageSize, pageToken:PageToken);
        PaginationTokens = messagesResponse.Headers["Link"].First().PaginationTokens();
        _messages = messagesResponse.Data;
        
        _isLoading = false;
    }
}