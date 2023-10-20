using Ory.Kratos.Client.Model;

namespace KratosSelfService.Models;

public record KratosUiTextMessageModel(KratosUiText UiText, string Content, string CssClass);