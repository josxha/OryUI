using Ory.Kratos.Client.Model;

namespace KratosSelfService.Extensions;

public static class GroupEnumExt
{
    public static string ToLocalTitleString(this KratosUiNode.GroupEnum instance, IOryElementsTranslator oryTranslator)
    {
        return instance switch
        {
            KratosUiNode.GroupEnum.Password => oryTranslator.Get("settings.title-password"),
            KratosUiNode.GroupEnum.Profile => oryTranslator.Get("settings.title-profile"),
            KratosUiNode.GroupEnum.Totp => oryTranslator.Get("settings.title-totp"),
            KratosUiNode.GroupEnum.LookupSecret => oryTranslator.Get("settings.title-lookup-secret"),
            KratosUiNode.GroupEnum.Webauthn => oryTranslator.Get("settings.title-webauthn"),
            KratosUiNode.GroupEnum.Oidc => oryTranslator.Get("settings.title-oidc"),
            _ => instance.ToString().ToLower()
        };
    }

    public static string ToLocalNavString(this KratosUiNode.GroupEnum instance, IOryElementsTranslator oryTranslator)
    {
        return instance switch
        {
            KratosUiNode.GroupEnum.Password => oryTranslator.Get("settings.navigation-password"),
            KratosUiNode.GroupEnum.Profile => oryTranslator.Get("settings.navigation-profile"),
            KratosUiNode.GroupEnum.Totp => oryTranslator.Get("settings.navigation-totp"),
            KratosUiNode.GroupEnum.LookupSecret => oryTranslator.Get("settings.navigation-backup-codes"),
            KratosUiNode.GroupEnum.Webauthn => oryTranslator.Get("settings.navigation-webauthn"),
            KratosUiNode.GroupEnum.Oidc => oryTranslator.Get("settings.navigation-oidc"),
            _ => instance.ToString().ToLower()
        };
    }

    public static string ToSiteAnchorTag(this KratosUiNode.GroupEnum instance)
    {
        return instance switch
        {
            KratosUiNode.GroupEnum.Password => "password",
            KratosUiNode.GroupEnum.Profile => "profile",
            KratosUiNode.GroupEnum.Totp => "totp",
            KratosUiNode.GroupEnum.LookupSecret => "lookupSecret",
            KratosUiNode.GroupEnum.Webauthn => "webauthn",
            KratosUiNode.GroupEnum.Oidc => "oidc",
            _ => instance.ToString().ToLower()
        };
    }

    public static string ToFaIcon(this KratosUiNode.GroupEnum instance)
    {
        return instance switch
        {
            KratosUiNode.GroupEnum.Password => "fa-lock",
            KratosUiNode.GroupEnum.Profile => "fa-user",
            KratosUiNode.GroupEnum.Totp => "fa-shield",
            KratosUiNode.GroupEnum.LookupSecret => "fa-list",
            KratosUiNode.GroupEnum.Webauthn => "fa-list",
            KratosUiNode.GroupEnum.Oidc => "fa-list",
            _ => instance.ToString().ToLower()
        };
    }
}