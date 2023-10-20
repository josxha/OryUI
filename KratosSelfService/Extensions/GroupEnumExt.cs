using Ory.Kratos.Client.Model;

namespace KratosSelfService.Extensions;

public static class GroupEnumExt
{
    public static string ToLocalString(this KratosUiNode.GroupEnum instance, IOryElementsTranslator oryTranslator)
    {
        return instance switch
        {
            KratosUiNode.GroupEnum.Password => oryTranslator.Get("settings.title-password"),
            KratosUiNode.GroupEnum.Profile => oryTranslator.Get("settings.title-profile"),
            KratosUiNode.GroupEnum.Totp => oryTranslator.Get("settings.title-totp"),
            KratosUiNode.GroupEnum.LookupSecret => oryTranslator.Get("settings.title-lookup-secret"),
            KratosUiNode.GroupEnum.Webauthn => oryTranslator.Get("settings.title-webauthn"),
            KratosUiNode.GroupEnum.Oidc => oryTranslator.Get("settings.title-oidc"),
            _ => instance.ToString()
        };
    }
}