using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KratosSelfService.Models;

public class ConsentBody
{
    [Required]
    [JsonPropertyName("consent_challenge")]
    public string Challenge { get; init; } = null!;

    [Required]
    [JsonPropertyName("consent_action")]
    public string Action { get; init; } = null!;

    [Required]
    [JsonPropertyName("remember")]
    public bool Remember { get; init; }

    [Required]
    [JsonPropertyName("grant_scope")]
    public string GrantScope { get; init; } = null!;
}