using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Users;

public partial class Index
{
    private List<KratosIdentity>? _identities;
    private bool _isLoading = true;

    [SupplyParameterFromQuery(Name = "page_token")]
    private string? PageToken { get; set; }
    private string? PageTokenFirst { get; set; }
    private string? PageTokenNext { get; set; }

    [SupplyParameterFromQuery(Name = "page_size")]
    private int PageSize { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        if (PageSize == 0) PageSize = 1; // TODO 100
        
        var identitiesResponse = await ApiService.KratosIdentity
            //.ListIdentitiesWithHttpInfoAsync(PerPage, PageNr);
            .ListIdentitiesWithHttpInfoAsync(PageSize);
        var linksRaw = identitiesResponse.Headers["Link"];
        // </admin/identities?page_size=1&page_token=00000000-0000-0000-0000-000000000000>; rel="first"
        // </admin/identities?page_size=1&page_token=1302cdef-30e6-490c-abec-0b6d3406158b>; rel="next"
        Console.WriteLine(string.Join("\n", linksRaw));
        _identities = identitiesResponse.Data;
        
        _isLoading = false;
    }
}