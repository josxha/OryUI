﻿using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using Ory.Kratos.Client.Client;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.Clients;

public partial class Edit
{
    private HydraOAuth2Client? _client;
    private string? _errorMessage;
    private bool _isLoading = true;

    [Parameter]
    public required string ClientId { get; set; }

    private readonly List<HydraJsonPatch> _patches = new();

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _client = await ApiService.HydraOAuth2.GetOAuth2ClientAsync(ClientId);
        _isLoading = false;
    }

    private async Task SubmitForm()
    {
        try
        {
            _ = await ApiService.HydraOAuth2.PatchOAuth2ClientAsync(ClientId, _patches);
        }
        catch (ApiException exception)
        {
            _errorMessage = exception.Message;
            return;
        }

        Navigation.NavigateTo("oauth2/clients");
    }

    private void AddPatch(HydraJsonPatch patch)
    {
        _patches.Add(patch);
    }
}