using Ory.Kratos.Client.Model;

namespace KratosSelfService.Models;

public record LoginModel(string FlowId, KratosLoginFlow Flow, string? ErrorMessage = null);