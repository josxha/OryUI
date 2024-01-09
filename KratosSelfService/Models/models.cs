using Newtonsoft.Json.Schema;
using Ory.Hydra.Client.Model;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Models;

public record ErrorModel(
    KratosFlowError? flow
);

public record LoginModel(
    KratosLoginFlow flow,
    string? forgotPasswordUrl,
    string signupUrl,
    string? logoutUrl
);

public record ProfileModel(
    KratosSession session,
    Dictionary<List<string>, JSchema> traitSchemas
);

public record LogoutModel(
    string logoutChallenge
);

public record RecoveryModel(
    KratosRecoveryFlow flow,
    string loginUrl
);

public record RegistrationModel(
    KratosRegistrationFlow flow,
    string loginUrl
);

public record SessionsModel(
    KratosSession CurrentSession,
    List<KratosSession> OtherSessions
);

public record SettingsModel(
    KratosSettingsFlow flow
);

public record VerificationModel(
    KratosVerificationFlow flow
);

public enum FlowType
{
    Settings,
    Login,
    Logout,
    Registration,
    Recovery,
    Verification
}

public record KratosUiTextMessageModel(
    KratosUiText UiText,
    string Content,
    string CssClass
);

public record KratosUiNodeArgs(
    KratosUiNode node,
    FlowType FlowType,
    string? forgotPasswordUrl = null
);

public record KratosUiArgs(
    KratosUiContainer ui,
    FlowType flowType,
    string? forgotPasswordUrl = null
);

public record KratosUiModel(
    KratosUiContainer ui,
    FlowType flowType,
    Dictionary<KratosUiNode.GroupEnum, List<KratosUiNode>> nodeGroups,
    List<KratosUiNode> defaultGroup,
    string? forgotPasswordUrl = null
);

public record ConsentModel(HydraOAuth2ConsentRequest request);