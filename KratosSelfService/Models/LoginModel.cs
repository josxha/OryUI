using Ory.Kratos.Client.Model;

namespace KratosSelfService.Models;

public class LoginModel(string flowId, KratosLoginFlow flow, string? errorMessage = null)
{
    public string FlowId { get; } = flowId;
    public KratosLoginFlow Flow { get; } = flow;
    public string? ErrorMessage { get; } = errorMessage;
}