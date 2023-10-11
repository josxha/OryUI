namespace KratosSelfService.Models;

public class LoginForm
{
    public string? Identifier { get; set; }
    public string? Password { get; set; }
    public bool RememberMe { get; set; }

    public override string ToString()
    {
        return $"Identifier: {Identifier}, Password: {Password}, RememberMe: {RememberMe}";
    }
}