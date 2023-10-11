using Ory.Client.Api;
using Ory.Client.Client;

namespace KratosAdmin.Components.Pages;

public partial class Home
{
    private string? _kratosAdminUrl;
    private string? _kratosPublicUrl;
    private string? _kratosReady;
    private string? _kratosVersion;

    protected override async Task OnInitializedAsync()
    {
        var metadataApi = new MetadataApi(new Configuration
        {
            BasePath = EnvService.KratosAdminUrl
        });
        _kratosPublicUrl = EnvService.KratosPublicUrl;
        _kratosAdminUrl = EnvService.KratosAdminUrl;
        try
        {
            var version = await metadataApi.GetVersionAsync();
            _kratosVersion = version._Version;
            var ready = await metadataApi.IsReadyAsync();
            _kratosReady = ready.Status;
        }
        catch (Exception exception)
        {
            _kratosVersion = exception.Message;
            _kratosReady = exception.Message;
        }
    }
}