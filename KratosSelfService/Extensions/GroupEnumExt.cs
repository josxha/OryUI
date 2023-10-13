using Ory.Kratos.Client.Model;

namespace KratosSelfService.Extensions;

public static class GroupEnumExt
{
    public static string ToLocalString(this KratosUiNode.GroupEnum instance, ICustomTranslator translator)
    {
        return instance switch
        {
            KratosUiNode.GroupEnum.Password => translator.GetTranslation("Change Password"),
            KratosUiNode.GroupEnum.Profile => translator.GetTranslation("Profile Settings"),
            KratosUiNode.GroupEnum.Totp => translator.GetTranslation("Manage 2FA TOTP Authenticator App"),
            KratosUiNode.GroupEnum.LookupSecret => translator.GetTranslation("Manage 2FA Backup Recovery Codes"),
            KratosUiNode.GroupEnum.Webauthn => translator.GetTranslation("WebAuthn Login"),
            _ => instance.ToString()
        };
    }
}