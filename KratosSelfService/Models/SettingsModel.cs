using Ory.Kratos.Client.Model;

namespace KratosSelfService.Models;

public record SettingsModel(KratosSettingsFlow flow, string postUri);