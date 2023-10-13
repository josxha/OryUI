using Ory.Kratos.Client.Model;

namespace KratosSelfService.Models;

public record SessionsModel(KratosSession CurrentSession, List<KratosSession> OtherSessions);