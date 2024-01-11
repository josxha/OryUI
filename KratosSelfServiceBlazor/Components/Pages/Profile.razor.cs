using KratosSelfServiceBlazor.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Profile
{
    private bool _isLoading = true;
    private KratosSession _session = default!;
    private Dictionary<List<string>, JSchema> _flatSchema = default!;
    private JSchema _schema = default!;
    private JObject _traits = default!;

    protected override async Task OnInitializedAsync()
    {
        _session = (KratosSession)accessor.HttpContext!.Items[typeof(KratosSession)]!;
        _traits = (JObject)_session.Identity.Traits;
        _schema = await schemaService.FetchSchema(_session.Identity.SchemaId,
            _session.Identity.SchemaUrl);
        _flatSchema = IdentitySchemaService.GetTraits(_schema);

        _isLoading = false;
    }
}