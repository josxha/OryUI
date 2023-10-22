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

public record LogoutModel(
    KratosLogoutFlow flow
);

public record RecoveryModel(
    KratosRecoveryFlow flow
);

public record RegistrationModel(
    KratosRegistrationFlow flow
);

public record SessionsModel(
    KratosSession CurrentSession,
    List<KratosSession> OtherSessions
);

public record SettingsModel(
    KratosSettingsFlow flow,
    string postUri
);

public record VerificationModel(
    KratosVerificationFlow flow
);

public record KratosUiTextMessageModel(
    KratosUiText UiText,
    string Content,
    string CssClass
);

public record KratosUiNodeModel(KratosUiNode node,
    KratosLoginFlow? loginFlow = null,
    string? forgotPasswordUrl = null
);