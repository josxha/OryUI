using OryAdmin.Models;

namespace OryAdmin.Components.Layout;

public partial class NavbarMenu
{
    private bool ServiceEnabled(OryService service)
    {
        return EnvService.ServiceEnabled(service);
    }
}