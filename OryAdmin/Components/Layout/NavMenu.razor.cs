using KratosAdmin.Models;

namespace KratosAdmin.Components.Layout;

public partial class NavMenu
{
    //[Inject] private EnvService EnvService { get; set; } = default!;

    private bool ServiceEnabled(OryService service)
    {
        return EnvService.ServiceEnabled(service);
    }
}