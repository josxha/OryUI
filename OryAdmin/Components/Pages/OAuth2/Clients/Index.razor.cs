using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using OryAdmin.Extensions;
using OryAdmin.Services;
using OryAdmin.Utils;

namespace OryAdmin.Components.Pages.OAuth2.Clients;

public partial class Index : ComponentBase
{
    private List<HydraOAuth2Client>? _clients;
    private bool _isLoading = true;
    
    [SupplyParameterFromQuery(Name = "page_token")]
    private string? PageToken { get; set; }

    private PaginationTokens PaginationTokens { get; set; } = null!;

    [SupplyParameterFromQuery(Name = "page_size")]
    private int PageSize { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        _isLoading = true;
        if (PageSize == 0) PageSize = 50;
        
        try
        {
            var clientsResponse = await ApiService.HydraOAuth2
                .ListOAuth2ClientsWithHttpInfoAsync(pageSize:PageSize, pageToken:PageToken);
            PaginationTokens = clientsResponse.Headers["Link"].First().PaginationTokens();
            _clients = clientsResponse.Data;
        }
        catch (Exception)
        {
            // ignored
        }

        _isLoading = false;
    }
}