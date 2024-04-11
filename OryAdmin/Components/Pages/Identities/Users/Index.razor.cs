using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Extensions;
using OryAdmin.Services;
using OryAdmin.Utils;

namespace OryAdmin.Components.Pages.Identities.Users;

public partial class Index
{
    private List<KratosIdentity>? _identities;
    private bool _isLoading = true;

    [SupplyParameterFromQuery(Name = "page_token")]
    private string? PageToken { get; set; }

    private PaginationTokens PaginationTokens { get; set; } = null!;

    [SupplyParameterFromQuery(Name = "page_size")]
    private int PageSize { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;

    /**
     * Using OnParametersSetAsync here to reload the page if the pagination page is changed
     */
    protected override async Task OnParametersSetAsync()
    {
        _isLoading = true;
        if (PageSize == 0) PageSize = 50;

        var identitiesResponse = await ApiService.KratosIdentity
            .ListIdentitiesWithHttpInfoAsync(pageSize:PageSize, pageToken:PageToken);
        PaginationTokens = identitiesResponse.Headers["Link"].First().PaginationTokens();
        _identities = identitiesResponse.Data;

        _isLoading = false;
    }
}